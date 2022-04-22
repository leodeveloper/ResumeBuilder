"use strict";

window.DevAV = window.DevAV || {};

DevAV.Employees = function () {
    var grid;
    var currentEmployee;
    var employeePopup;
    var vacancyApplyPopup;
    var trainingApplyPopup;
    var vacancyApplyPopupPreviousHistory;
    var trainingApplyPopupPreviousHistory;
    var notesPopup;
    var coverLetterPopup;
    var currentNote;
    var postlistVacancy = [];
    var postlistTraining = [];
    var assessmentPopup;   
    var integrationPopup;
    var filterPopup;
    var CuurentTabIndex = 0;

    this.sectionsData = [{
        id: 0,
        text: "Evaluations",
        html: "<div id='notes-grid'></div>"
    }, {
        id: 1,
        text: "Tasks",
        html: "<div id='tasks-grid'></div>"
    }];

    var formSelector = "#details-form";
    var formseletorspecialNeeds = "#speicalNeeds-form";

    this.formatPhoneNumber = function (data) {
        if (typeof (data) === "object") {
            data = data.value;
        }

        return data.replace(/(\d{3})(\d{3})(\d{4})/, "+1($1)$2-$3");
    };

    this.editNote = function (data) {
        currentNote = data;

        notesPopup.option("visible", true);
    };

    this.setEditNoteValues = function () {
        $("#note-edit-label")
            .text("Edit Notes" + (currentNote.Reviewer.Employee_ID ? " (" + currentNote.Reviewer.Employee_Full_Name + ")" : ""));

        $("#notes-form").dxForm("instance").updateData(currentNote);
    };

    this.saveNoteChanges = function () {
        if (!DevAV.isFormValid("#notes-form")) return;

        notesPopup.hide();
        DevAV.showDBNotice();
    };

    this.deleteNote = function () {
        notesPopup.hide();
        DevAV.showDBNotice();
    };

    this.hideNotesPopup = function () {
        notesPopup.hide();
    };

    this.tabItemRendered = function (e) {       
        if (e.itemIndex === 12) {           
            DevAV.employees.getSpecialNeeds();
        }
       
    };

    var updateEmployeePanel = function () {       
        $("#selected_resume_Id").val(currentEmployee.Rid);      
    };


    this.selectRow = function (data) {
        var rowData = data.selectedRowsData[0];
        if (!rowData) return;
        currentEmployee = rowData;
        
        updateEmployeePanel();
        getResumeId();
        DevAV.employees.getJobSeekerPreview();        
        $("#jobseekername").text(currentEmployee.FullName);
        $("#jobseekernameAr").text(currentEmployee.FullNameAr);
        $("#detailjobseekerFullName").text(currentEmployee.FullName);
        $("#detailjobseekerFullNameAr").text(currentEmployee.FullNameAr);
        

        $("#jobseekercode").text(currentEmployee.JobSeekerId);
       // $("#jobseekeremail").text(currentEmployee.EmailId);
        $("#jobseekerstatus").text(currentEmployee.StatusTitle);
        $("#jobseekerstatusAr").text(currentEmployee.StatusTitleAr);

       
        $("#education-grid").dxDataGrid("refresh");
        $("#employer-grid").dxDataGrid("refresh");
        $("#reference-grid").dxDataGrid("refresh");
        $("#certification-grid").dxDataGrid("refresh");        
        $("#source-grid").dxDataGrid("refresh");
        $("#notes-grid").dxDataGrid("refresh");
        $("#status-grid").dxDataGrid("refresh");
        $("#attachment-grid").dxDataGrid("refresh");
        $("#jobapplcation-attachment-grid").dxDataGrid("refresh");
        $("#assessment-grid").dxDataGrid("refresh");
        $("#occupation-grid").dxDataGrid("refresh");
        $("#toolsAndKnowledge-grid").dxDataGrid("refresh");
        $("#language-grid").dxDataGrid("refresh");
        $("#jobseekergrieveance-grid").dxDataGrid("refresh");
        DevAV.employees.getSpecialNeeds();
        //Populate Report
        $("#answer-result-report-grid").dxDataGrid("refresh");
        $("#answer-result-report-grid1").dxDataGrid("refresh");
        $("#answer-result-report-grid2").dxDataGrid("refresh");
        $("#answer-result-report-grid3").dxDataGrid("refresh");
        $("#answer-result-report-grid4").dxDataGrid("refresh");

        //Detail section       
       // $("#detailjobseekercode").text(currentEmployee.JobSeekerId);
        $("#detailEmiratesID").text(currentEmployee.EmiratesId);
        $("#detailPassportNumber").text(currentEmployee.PassportNumber);
        $("#detailMobileNumber").text(currentEmployee.MobilePhone);
       // $("#detailLandLineNumber").text(currentEmployee.LandLine);
        $("#detailEmail").text(currentEmployee.EmailId);
      //  $("#detailMilitaryServiceBatch").text(currentEmployee.MilitaryServiceBatch);
        var _dob = new Date(currentEmployee.DOB);

        $("#detailDob").text(formatDate(_dob));
        $("#detailCreatedDate").text(currentEmployee.CreatedDate);
        $("#detailModifiedDate").text(currentEmployee.LastUpdateDate);
        //end Detail Section
    };

   

    //When the status changes from the status grid need to update the Resumelist grid
    this.reloadResumeGrid = function () {
        $("#grid").dxDataGrid("instance").getDataSource().reload();
    };

    this.selectFirstRow = function () {
        grid.selectRowsByIndexes([0]);
    };

    this.hideDetailsPopup = function () {
        employeePopup.hide();
    };

    this.saveEmployee = function () {
        if (!DevAV.isFormValid(formSelector)) {
            return;
        }
        else {
            $("#saveandupdate").submit();
        }       
    };

    

    this.getJobSeekerPreview = function () {
        $.ajax({
            url: "api/JobSeekerResumes/GetJobSeekerPreview?resumeid=" + parseInt(getResumeId()),
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {            
            if (response) {
              
                $("#jobseekerTotalExp").text(response.TotalExperience);
                $("#jobseekerAge").text(response.Age);

                $("#detailEmirates").text(response.OtherJobSeekerInfo.Emirates);
                $("#detailCity").text(response.OtherJobSeekerInfo.City);

                $("#detailLocation").text(response.OtherJobSeekerInfo.Location);
                $("#detailMartialStatus").text(response.OtherJobSeekerInfo.MartialStatus);
                $("#detailGender").text(response.OtherJobSeekerInfo.Gender);

                if (response.JobSeekerCvphoto.Base64fileContentWithContentType) {
                    $('#jobSeekerPhoto').attr('src', response.JobSeekerCvphoto.Base64fileContentWithContentType + response.JobSeekerCvphoto.PhotoContent);
                }
                else {
                    $('#jobSeekerPhoto').attr('src', 'images/user.png');
                }
                $("#jobseekerHighestEducation").text(response.HighestEducation);
                $("#jobseekerCurrentRole").text(response.CurrentRole);
               // response;
            }
           
        });//.fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "JobSeeker photo fail", width: 300, shading: false }, "error", 1000); });
    }    

    this.saveSpeicalNeeds = function () {

        let myform = $(formseletorspecialNeeds).dxForm("instance");
        let _specialNeeds = myform.getEditor("SpeicalNeeds_ID").option("value");    
        $.ajax({
            url: "api/PeopleofDetermination/InsertDeletePeopleofDetermination",
            type: "POST",
            dataType: "json",
            data: JSON.stringify({ "Resume_ID": parseInt(getResumeId()), "SpeicalNeeds_ID": _specialNeeds}),
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {
            if (response) {
                DevExpress.ui.notify({ message: "Save success", width: 300, shading: false }, "success", 1000);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Save fail", width: 300, shading: false }, "error", 1000); });
    };

    this.getSpecialNeeds = function () {

        let sepcialNeedForm = $(formseletorspecialNeeds).dxForm("instance");
        if (typeof (sepcialNeedForm) !== "undefined" && sepcialNeedForm !== null) {
            let newvalue = []
            sepcialNeedForm.updateData("SpeicalNeeds_ID", newvalue);
            $.ajax({
                url: "api/PeopleofDetermination/GetAllPeopleofDeterminationResume?resumeid=" + parseInt(getResumeId()),
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.data) {
                        let listoftoolsknowledge = $.map(response.data, function (value) {
                            newvalue.push(value.SpeicalNeeds_ID);
                            return value.SpeicalNeeds_ID;
                        }).join(", ");
                        // console.log(newvalue);
                        sepcialNeedForm.updateData("SpeicalNeeds_ID", newvalue);
                    }
                }
            });
        }
    };

    this.saveEmployee = function () {
        if (!DevAV.isFormValid(formSelector)) {
            return;
        }
        else {
            $("#saveandupdate").submit();
        }
    };

    this.deleteEmployee = function () {
        DevAV.showDBNotice();
    };

    this.editEmployeePopup = function () {
        showDetailsPopup(currentEmployee);
    }

    this.pdfdownloadAndPreview = function (pdfUrl) {
        //console.log(pdfUrl);
        //console.log(currentEmployee);
        window.open(pdfUrl + currentEmployee.Rid, '', 'window settings');
      //  window.open('http://172.19.181.143:5500/jsreport.html', '_blank').focus();
        return false;
    }

    var showDetailsPopup = function (data) {
        
        employeePopup.show();
        var form = $(formSelector).dxForm("instance");        
        form.option({
            formData: data
        });

        var labelText;

        if (data) {
            labelText = "Edit";// (" + data.Employee_Full_Name + ")";
            //form.getEditor("EmiratesId").option("readonly", "true");
           // console.log(form.getEditor("EmiratesId"));
        } else {
            labelText = "Create";
        }

        $("#grid-details-label").text(labelText);
    };

    this.createNewEmployee = function () {
        showDetailsPopup();
    };

    this.gridDetailsTemplate = function (element, options) {
        $("<div>")
            .addClass("grid-details")
            .on("dxclick", function (e) {
                showDetailsPopup(options.data);
            })
            .appendTo(element);
    };
    //Vacancy Apply

    this.ShowApplyVacancyPopupButtonClick = function () {
        DevAV.employees.ShowApplyVacancyPopup(currentEmployee);
    }

    this.ShowApplyVacancyPopupPreviousHistoryButtonClick = function () {
        DevAV.employees.ShowApplyVacancyPopupPreviousHistory(currentEmployee);
    }

    this.ShowApplyVacancyPopup = function (data) {   
        vacancyApplyPopup.show();
        $("#jobseeker_Id").val(data.Rid)
        $("#vacancy-jobseeker-name").text(data.FullName);// + "" + data.row.data.FullNameAr);
        $("#apply-vacancy-grid").dxDataGrid("instance").getDataSource().reload();
        $("#apply-vacancy-grid").dxDataGrid("instance").clearSelection();    
    };

    this.hideApplyVacancyPopup = function () {
        $("#apply-vacancy-grid").dxDataGrid("instance").clearSelection();
        vacancyApplyPopup.hide();
       
    };

    this.ShowApplyVacancyPopupPreviousHistory = function (data) {
        vacancyApplyPopupPreviousHistory.show();
        $("#jobseeker_Id").val(data.Rid)
        $("#vacancy-jobseeker-name").text(data.FullName);// + "" + data.row.data.FullNameAr);       

        $("#applied-vacancy-grid").dxDataGrid("instance").getDataSource().reload();
        $("#applied-vacancy-grid").dxDataGrid("instance").clearSelection();
    };

    this.hideApplyVacancyPopupPreviousHistory = function () {
        $("#applied-vacancy-grid").dxDataGrid("instance").clearSelection();
        vacancyApplyPopupPreviousHistory.hide();

    };

    this.SelectedVacancy = function (selectedItems) {
        var data = selectedItems.selectedRowsData;
        postlistVacancy = [];
        if (data.length > 0)
            $("#selectedItemsContainer").text(
                $.map(data, function (value) {                    
                    postlistVacancy.push(value.Id);
                    return value.AutoGenerateJobCode + "--" + value.Id;
                }).join(", "));
        else
            $("#selectedItemsContainer").text("No vacancy has been selected");     

        

        //disable if the traning already applied
        selectedItems.currentSelectedRowKeys.forEach((key) => {
            selectedItems.component.byKey(key).done((item) => {
                if (item.AppliedVacancyStatus == true)
                    selectedItems.component.deselectRows(key);
            });
        });

        if (postlistVacancy.length > 0) {
            $("#apply_vacancy_button").dxButton("instance").option("disabled", false);
        }
        else {
            $("#apply_vacancy_button").dxButton("instance").option("disabled", true);
        }
    };  

    this.postVacancy = function () {
        var jsonData = JSON.stringify({ "JobSeekerId": parseInt(getJobSeekerId()), "listVacancy": postlistVacancy });         
        $.ajax({
            url: "api/Vacancy/PostVacancies",
            type: "POST",
            dataType: "json",
            data: jsonData,
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {
            $("#apply-vacancy-grid").dxDataGrid("instance").getDataSource().reload();
            $("#apply-vacancy-grid").dxDataGrid("instance").clearSelection();

            $("#applied-vacancy-grid").dxDataGrid("instance").getDataSource().reload();
            $("#applied-vacancy-grid").dxDataGrid("instance").clearSelection();

            
           
                DevExpress.ui.notify({ message: "Apply success", width: 300, shading: true }, "success", 1000);
            
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Apply fail", width: 300, shading: true }, "error", 1000); });
    };
    //End vacancy

    //Training Apply

    this.ShowApplyTrainingPopupButtonClick = function () {
        DevAV.employees.ShowApplyTrainingPopup(currentEmployee);
    }

    this.ShowApplyTrainingPopupPreviousHistoryButtonClick = function () {
        DevAV.employees.ShowApplyTrainingPopupPreviousHistory(currentEmployee);
    }

    this.ShowApplyTrainingPopup = function (data) {
        trainingApplyPopup.show();
        $("#jobseeker_Id").val(data.Rid)
        $("#training-jobseeker-name").text(data.FullName);// + "" + data.row.data.FullNameAr);
        $("#apply-training-grid").dxDataGrid("instance").getDataSource().reload();
        $("#apply-training-grid").dxDataGrid("instance").clearSelection();
    };

    this.hideApplyTrainingPopup = function () {
        $("#apply-training-grid").dxDataGrid("instance").clearSelection();
        trainingApplyPopup.hide();

    };

    this.ShowApplyTrainingPopupPreviousHistory = function (data) {
        trainingApplyPopupPreviousHistory.show();
        $("#jobseeker_Id").val(data.Rid)
        $("#training-jobseeker-name").text(data.FullName);// + "" + data.row.data.FullNameAr);
        $("#applied-training-grid").dxDataGrid("instance").getDataSource().reload();
        $("#applied-training-grid").dxDataGrid("instance").clearSelection();
    };

    this.hideApplyTrainingPopupPreviousHistory = function () {
        $("#appllied-training-grid").dxDataGrid("instance").clearSelection();
        trainingApplyPopupPreviousHistory.hide();

    };

    this.SelectedTraining = function (selectedItems) {
        var data = selectedItems.selectedRowsData;       
        postlistTraining = [];
        if (data.length > 0)

            $("#selectedItemsContainer").text(
                $.map(data, function (value) {                   

                    postlistTraining.push(value.ID);
                    return value.ID;
                }).join(", "));
        else
            $("#selectedItemsContainer").text("No training has been selected");



        //disable if the traning already applied
        selectedItems.currentSelectedRowKeys.forEach((key) => {
            selectedItems.component.byKey(key).done((item) => {
                if (item.Is_AlReadyEnrol == true)
                    selectedItems.component.deselectRows(key);
            });
        });

        if (postlistTraining.length > 0) {
            $("#apply_trainingbatch_button").dxButton("instance").option("disabled", false);
        }
        else {
            $("#apply_trainingbatch_button").dxButton("instance").option("disabled", true);
        }

    };

    this.postTraining = function () {
        var jsonData = JSON.stringify({ "Resume_ID": parseInt(getJobSeekerId()), "Batch_ID": postlistTraining });      
        $.ajax({
            url: "api/Training/PostTrainingBatchs",
            type: "POST",
            dataType: "json",
            data: jsonData,
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {
            $("#apply-training-grid").dxDataGrid("instance").getDataSource().reload();
            $("#apply-training-grid").dxDataGrid("instance").clearSelection();

            DevExpress.ui.notify({ message: "Apply success", width: 300, shading: true }, "success", 1000);

        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Apply fail", width: 300, shading: true }, "error", 1000); });
    };
    //End training
    

    this.init = function () {
        employeePopup = $("#details-form-popup").dxPopup("instance");
        coverLetterPopup = $("#coverLetter-popup").dxPopup("instance");
        vacancyApplyPopup = $("#vacancy-applied-popup").dxPopup("instance");
        trainingApplyPopup = $("#training-applied-popup").dxPopup("instance");
        vacancyApplyPopupPreviousHistory = $("#vacancy-applied-popup-previous-history").dxPopup("instance");        
        trainingApplyPopupPreviousHistory = $("#training-applied-popup-previous-history").dxPopup("instance");
        assessmentPopup = $("#assessment-popup").dxPopup("instance");
       
        integrationPopup = $("#integration-popup").dxPopup("instance"); 
        filterPopup = $("#resumelistfilter-popup").dxPopup("instance");
        grid = $("#grid").dxDataGrid("instance");

        $("#edit-employee-icon").on("dxclick", function () {
            showDetailsPopup(currentEmployee);
        });
    };


    //Status 
    var getStatusPopup = function () {
        return $("#status-popup").dxPopup("instance");
    };

    var getStatusForm = function () {
        return $("#status-form").dxForm("instance");
    }; 

    this.statusTemplate = function (e) {

        getStatusPopup().show();
        getStatusForm().updateData(e.row.data);
        e.component.refresh(true);
        e.event.preventDefault();
    };

    this.editStatus = function (statusData) {
        $("#updateResumeStatus").submit();       
    };   

    this.cancelChanges = function () {
        getStatusPopup().hide();
    };
   //EndStatus
    //Assessment
    this.ShowAssessmentPopup = function (data) {
        
        $("#assessment-template-id").text(data.row.data.ID);
        $("#assessment-jobseeker-name").text(currentEmployee.FullName);// + "" + data.row.data.FullNameAr);
        $("#current-templateId").val(data.row.data.ID);    
       
        assessmentPopup.show();
        $("#answer-grid").dxDataGrid("instance").getDataSource().reload();
    };
    this.hideAssessmentPopup = function () {
        $("#answer-grid").dxDataGrid("instance").clearSelection();
        assessmentPopup.hide();
    };
    //EndAssessment
    //CoverLetter
    this.ShowCoverLetterPopup = function (data) {
       
        coverLetterPopup.show();   
        DevAV.employees.getCoverLetter();
        
    };
    this.hideCoverLetterPopup = function () {        
        coverLetterPopup.hide();
    };

    this.getCoverLetter = function () {

        let _jobSeekercoverLetterForm = $("#jobSeekerCoverLetter-form").dxForm("instance");
        let _coverLetter = _jobSeekercoverLetterForm.getEditor("CoverLetter");
        let _accomplishments = _jobSeekercoverLetterForm.getEditor("Accomplishments");
        _coverLetter.option("value", "");
        _accomplishments.option("value", "");
        $.ajax({
            url: "api/CoverLetter/GetCoverLeter?resume_ID="+parseInt(getResumeId()),
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {
            if (response) {
                _coverLetter.option("value", response.data.CoverLetter);
                _accomplishments.option("value", response.data.Accomplishments);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Save fail", width: 300, shading: false }, "error", 1000); });
    };

    this.saveCoverLetter = function () {

        let _jobSeekercoverLetterForm = $("#jobSeekerCoverLetter-form").dxForm("instance");
        if (_jobSeekercoverLetterForm.validate().isValid) {
            let _coverLetter = _jobSeekercoverLetterForm.getEditor("CoverLetter").option("value");
            let _accomplishments = _jobSeekercoverLetterForm.getEditor("Accomplishments").option("value");
            $.ajax({
                url: "api/CoverLetter/InsertUpdateCoverLeter",
                type: "POST",
                dataType: "json",
                data: JSON.stringify({ "Resume_ID": parseInt(getResumeId()), "CoverLetter": _coverLetter, "Accomplishments": _accomplishments }),
                contentType: "application/json; charset=utf-8"
            }).done(function (response) {
                if (response) {
                    DevExpress.ui.notify({ message: "Save success", width: 300, shading: false }, "success", 1000);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Save fail", width: 300, shading: false }, "error", 1000); });

        }
    };
   //EndCoverLetter
    //Integration
    this.ShowIntegrationPopupButtonClick = function()
    {
        DevAV.employees.ShowIntegrationPopup(currentEmployee);
    }
    this.ShowIntegrationPopup = function (data) {
        integrationPopup.show();
        $("#jobseeker_Id").val(data.Rid);
        $("#integration-jobseeker-name").text(data.FullName);
        Integration.employees.loadPartial(data.Rid);
        $("#loadingIntegration").hide();
    };
    this.hideIntegrationPopup = function () {
        $("#integration-personalInfo-username").text("");
        $("#integration-personalInfo-lastupdate").text("");
        integrationPopup.hide();
    };
    //EndIntegration
    //Resume list filter popup
    this.ShowResumeFilterPopup = function () {
        filterPopup.show();        
    };
    this.hideResumeFilterPopup = function () {      
        filterPopup.hide();
    };
    //End Resume list filter popup
    //customer search box
    this.filterDataByText = function (searchData) {
        const value = searchData.target.value;
        if (value.length > 3) {
            grid.searchByText(value);
        }
        if (value.length == 0) {
            grid.searchByText(value);
        }
    };

    //End
};


DevAV.employees = new DevAV.Employees();
DevAV.isFormValid = function (selector) {
    var form = $(selector).dxForm("instance");

    //For insertion set Rid=0
    var _rid = form.getEditor("Rid").option("value");
    if (_rid === "" || _rid === null) {
        form.updateData("Rid", "0");
        form.updateData("StatusId", "1");
        //Inital default resumestatus, when insert
        form.updateData("resumestatus", "1");
    }
    //end
    return form.validate().isValid;
};
DevAV.screenByWidth = function () {
    return "lg";
};

$(function () {
    DevAV.employees.init();
});

function onInitNewRow(e) {
    e.data["Resume_ID"] = getResumeId();
}  

function onInitNewRowAttachment(e) {
    e.data["Resume_ID"] = getResumeId();
    var uniqueID = Math.floor(Math.random() * 26) + Date.now();
    e.data["MongoDBUniqueId"] = uniqueID;
    $("#selected_attachment_Id").val(uniqueID);
    //
} 

function onInitNewRowAttachmentForJobseekerGrivences(e) {
    e.data["Resumeid"] = getResumeId();
    var uniqueID = Math.floor(Math.random() * 26) + Date.now();
    e.data["MongoDBUniqueId"] = uniqueID;
    $("#selected_attachment_Id").val(uniqueID);
    //
} 

function getResumeId() {
   return $("#selected_resume_Id").val();
}
//This is for apply vacancy
function getJobSeekerId() {
    return $("#jobseeker_Id").val();
}

//This is for apply vacancy
function getTemplateId() {
    return $("#current-templateId").val();
}

//This is for mongo db file attachment
function getAttachmentId() {
    return $("#selected_attachment_Id").val();
}

function getEmiratesId() {
    
    return $("#detailEmiratesID").text();
}

//export excel

function exportGrids() {
   
    ($("#grid").length
        ? $("#grid").dxDataGrid("instance")
        : $("#grid").dxDataGrid("instance")
    ).exportToExcel();

   
}


//function clear all tab grid
function clearTabGrid(gridName) {

  //  let ds = $("#status-grid").dxDataGrid("instance").getDataSource();//.option("dataSource"); // Save the dataSource
    let ds = $("#status-grid").dxDataGrid("instance");//.getDataSource();//.reload();
  //  console.log(ds);
    ds.option("dataSource", null); // Clear it
    ds.option("dataSource", ds); // Reassign it
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

function getAnswerResultsReport(emiratesID) {
    $.ajax({
        url: "api/Assessment/GetAnswerResultsReport?emiratesID=" + emiratesID,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (response) {
        if (response) {
          //  console.log(emiratesID);
         //   console.log(response);

          
        }
    }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Save fail", width: 300, shading: false }, "error", 1000); });

}
