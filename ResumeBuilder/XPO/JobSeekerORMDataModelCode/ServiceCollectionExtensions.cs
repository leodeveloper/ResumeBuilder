﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ResumeBuilder.XPO.Database;
namespace ResumeBuilder.XPO.Database
{
    public partial class DatabaseUnitOfWork : UnitOfWork
    {
        public DatabaseUnitOfWork() : base() { }
        public DatabaseUnitOfWork(DevExpress.Xpo.Metadata.XPDictionary dictionary) : base(dictionary) { }
        public DatabaseUnitOfWork(IDataLayer layer, params IDisposable[] disposeOnDisconnect) : base(layer, disposeOnDisconnect) { }
        public DatabaseUnitOfWork(IObjectLayer layer, params IDisposable[] disposeOnDisconnect) : base(layer, disposeOnDisconnect) { }
    }
}
namespace Microsoft.Extensions.DependencyInjection
{
    //public static class DatabaseXpoServiceCollectionExtensions
    //{
    //    public static IServiceCollection AddDatabaseAsXpoDefaultUnitOfWork(this IServiceCollection serviceCollection, bool useDataLayerAsSingletone = true, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoDefaultUnitOfWork(useDataLayerAsSingletone, optionsBuilder);
    //    }
    //    public static IServiceCollection AddDatabaseAsXpoDefaultSession(this IServiceCollection serviceCollection, bool useDataLayerAsSingletone = true, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoDefaultSession(useDataLayerAsSingletone, optionsBuilder);
    //    }
    //    public static IServiceCollection AddDatabaseUnitOfWork(this IServiceCollection serviceCollection, bool useDataLayerAsSingletone = true, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoCustomSession<DatabaseUnitOfWork>(useDataLayerAsSingletone, optionsBuilder);
    //    }
    //    public static IServiceCollection AddDatabaseXpoDefaultDataLayer(this IServiceCollection serviceCollection, ServiceLifetime lifetime, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoDefaultDataLayer(lifetime, customDataLayerOptionsBuilder);
    //    }
    //    public static IServiceCollection AddDatabaseAsXpoDefaultUnitOfWork(this IServiceCollection serviceCollection, IConfiguration configuration, bool useDataLayerAsSingletone = true, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoDefaultUnitOfWork(useDataLayerAsSingletone, o => { o.UseConnectionStringForDatabase(configuration); optionsBuilder(o); });
    //    }
    //    public static IServiceCollection AddDatabaseAsXpoDefaultSession(this IServiceCollection serviceCollection, IConfiguration configuration, bool useDataLayerAsSingletone = true, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoDefaultSession(useDataLayerAsSingletone, o => { o.UseConnectionStringForDatabase(configuration); optionsBuilder(o); });
    //    }
    //    public static IServiceCollection AddDatabaseUnitOfWork(this IServiceCollection serviceCollection, IConfiguration configuration, bool useDataLayerAsSingletone = true, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoCustomSession<DatabaseUnitOfWork>(useDataLayerAsSingletone, o => { o.UseConnectionStringForDatabase(configuration); optionsBuilder(o); });
    //    }
    //    public static IServiceCollection AddDatabaseXpoDefaultDataLayer(this IServiceCollection serviceCollection, IConfiguration configuration, ServiceLifetime lifetime, Action<DataLayerOptionsBuilder> customDataLayerOptionsBuilder = null)
    //    {
    //        Action<DataLayerOptionsBuilder> optionsBuilder = CreateDataLayerOptionsBuilder(customDataLayerOptionsBuilder);
    //        return serviceCollection.AddXpoDefaultDataLayer(lifetime, o => { o.UseConnectionStringForDatabase(configuration); optionsBuilder(o); });
    //    }
    //    public static DataLayerOptionsBuilder UseConnectionStringForDatabase(this DataLayerOptionsBuilder options, IConfiguration configuration)
    //    {
    //        return options.UseConnectionString(configuration.GetConnectionString(ConnectionHelper.ConnectionStringName));
    //    }
    //    static Action<DataLayerOptionsBuilder> CreateDataLayerOptionsBuilder(Action<DataLayerOptionsBuilder> injectCustomDataLayerOptionsBuilder)
    //    {
    //        return (options) =>
    //        {
    //            options
    //            .UseEntityTypes(ConnectionHelper.GetPersistentTypes());
    //            if (injectCustomDataLayerOptionsBuilder != null)
    //            {
    //                injectCustomDataLayerOptionsBuilder(options);
    //            }
    //        };
    //    }
    //}
}
