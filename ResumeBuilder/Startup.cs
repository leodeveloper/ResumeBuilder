using FormFactory;
using IntegrationApiClassLibrary.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Helper;
using ResumeBuilder.Models;
using ResumeBuilder.Repository;
using ResumeBuilder.Repository.HraOpsRepository;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ResumeBuilder
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            configuration.GetSection("JobSeekerRoles").Bind(JobSeekerRolesSettings.JobSeekerRolesSetting);
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment HostingEnvironment { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerDocument();

            services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            //formFactory
            services.AddControllersWithViews()
               .AddRazorRuntimeCompilation(options => options.FileProviders.Add(
                   new EmbeddedFileProvider(typeof(FormFactory.FF).GetTypeInfo().Assembly, nameof(FormFactory))
               ));

            var embeddedProvider = new EmbeddedFileProvider(typeof(FF).Assembly, nameof(FormFactory));
            services.AddSingleton<IFileProvider>(embeddedProvider);
            //endformfactory

            services.AddRazorPages().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddControllers();
            services.AddOptions();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.Configure<DBConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<LookUpApiUrl>(Configuration.GetSection("LookUpApiUrl"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<OracleAppSettings>(Configuration.GetSection("OracleAppSettings"));
            //services.Configure<JobSeekerRoles>(Configuration.GetSection("JobSeekerRoles"));

            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));


            var mongdoDBSettings = new MongoDBSettings();
            Configuration.GetSection(key: nameof(MongoDBSettings)).Bind(mongdoDBSettings);
            services.AddSingleton(mongdoDBSettings);

            services.AddSingleton<IMongoClient, MongoClient>(
                client =>
            {
                return new MongoClient(mongdoDBSettings.connectionstring);
            }
            );


            string connectionString = Configuration.GetConnectionString("JobSeekerDatabase"); 
            string HcConnectionString = Configuration.GetConnectionString("HCDatabase");

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            //   services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();

            services.AddTransient(typeof(IGenericRepositoryPaggingDapper<>), typeof(GenericRepositoryPaggingDapper<>));
            services.AddTransient<IResumeRepository, ResumeRepository>();
            services.AddTransient<IEducationRepository, EducationRepository>();
            services.AddTransient<IEmployerRepository, EmployerRepository>();
            services.AddTransient<IReferenceRepository, ReferenceRepository>();
            services.AddTransient<ITrainingRepository, TrainingRepository>();
            services.AddTransient<ICertificationRepository, CertificationRepository>();
            services.AddTransient<ISourceRepository, SourceRepository>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<ILanguagesRepository, LanguagesRepository>();
            services.AddTransient<IlookupRespository, lookupRespository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IOccupationRepository, OccupationRepository>();
            services.AddTransient<IToolsKnowledgeRepository, ToolsKnowledgeRepository>();
            services.AddTransient<IPeopleofDeterminationRepository, PeopleofDeterminationRepository>();
            services.AddTransient<IJobSeekerGrieveanceRepository, JobSeekerGrieveanceRepository>();
            services.AddTransient<IHraOpsLookupRepository, HraOpsLookupRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>(service => new UnitOfWork(connectionString));
            services.AddTransient<IUnitOfWorkHra, UnitOfWorkHra>(service => new UnitOfWorkHra(HcConnectionString));

            services.AddDbContext<JobSeekerContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IHcResumeRepository, HcResumeRepository>();
            services.AddTransient<IHcResumeEducationRepository, HcResumeEducationRepository>();
            services.AddTransient<IHcResumeSkillRepository, HcResumeSkillRepository>();
            services.AddTransient<IHCResumeSkillGroupRepository, HCResumeSkillGroupRepository>();
            services.AddTransient<IHcResumeLanguagesRepository, HcResumeLanguagesRepository>();
            services.AddTransient<IHcResumeEmployerRepository, HcResumeEmployerRepository>();
            services.AddTransient<IHcResumePodRepository, HcResumePodRepository>();
            services.AddTransient<IHcResumeEngagementRepository, HcResumeEngagementRepository>();
            services.AddTransient<IHcResumeCertificationsRepository, HcResumeCertificationsRepository>();
            services.AddTransient<IHcResumeReferenceRepository, HcResumeReferenceRepository>();
            services.AddTransient<IHcResumeCoverLetterRepository, HcResumeCoverLetterRepository>();
            services.AddTransient<IHcResumePreferenceCityRepository, HcResumePreferenceCityRepository>();
            services.AddTransient<IHcResumePreferenceIndustryRepository, HcResumePreferenceIndustryRepository>();
            services.AddTransient<IHcResumePreferenceEscoJobTitleRepository, HcResumePreferenceEscoJobTitleRepository>();
            services.AddTransient<IHcResumeBeneficiaryRepository, HcResumeBeneficiaryRepository>();
            services.AddTransient<IAssessmentRepository, AssessmentRepository>();
            services.AddTransient<IPersonalInfoRepository, PersonalInfoRepository>();
            services.AddTransient<IIntegrationRepository, IntegrationRepository>();
            services.AddTransient<IPensionfundRepository, PensionfundRepository>();
            services.AddScoped<IIntegrationLogRepository, IntegrationLogRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITypeSenceApiRepository, TypeSenceApiRepository>();
           
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ar-AE") };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedUICultures = supportedCultures;
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
              builder =>
              {
                  builder
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                  //.AllowCredentials(); //AllowCrdentials and Alloworining with * cannot be together.
              }));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ar-AE") };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedUICultures = supportedCultures;
            });

            services.AddMemoryCache();

            services.AddHttpClient("LookUPApi", client =>
            {

                // client.BaseAddress = new Uri("");
                // client.DefaultRequestHeaders.Add();

            });

            services.AddHttpClient("IntegrationApi", client =>
            {
                client.BaseAddress = new Uri(Configuration["ApiUrl:GradeApiBaseUrl"]);
                client.Timeout = TimeSpan.FromSeconds(15);
            });
            services.AddHttpClient("IntegrationApiWithPort", client =>
            {
                client.BaseAddress = new Uri(Configuration["ApiUrl:GradeApiBaseUrlPort"]);
                client.Timeout = TimeSpan.FromSeconds(15);
            });
            // services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                      {
                          //config.Cookie.HttpOnly = true;
                          //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                          config.Cookie.SameSite = SameSiteMode.Lax;
                          config.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                          config.LoginPath = Configuration["LoginURL"];
                          config.LogoutPath = Configuration["LogoutURL"];
                      }).AddJwtBearer(options =>
                      {
                          options.SaveToken = true;
                          options.RequireHttpsMetadata = false;
                          options.TokenValidationParameters = JWTAddExtension.tokenValidationParameters(Configuration);
                      });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"c:\\Logs\\Resumebuilder.txt");
            app.UseCors("CorsPolicy");
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //For form factory
            var embeddedProvider = new EmbeddedFileProvider(typeof(FF).Assembly, nameof(FormFactory));
            var physicalFileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot"));
            var compositeProvider = new CompositeFileProvider(embeddedProvider, physicalFileProvider);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = compositeProvider,
                RequestPath = new PathString("")
            });
            //end form factory

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseSwaggerUi3();
            app.UseOpenApi();
        }
    }
}
