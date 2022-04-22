"use strict";

window.Integration = window.Integration || {};

Integration.Employees = function () {
   
    this.personalInfo = function () {
        $("#loadingIntegration").show();
        $.ajax({
            url: "api/Integration/UpdatePersonalInfo?resumeId=" + parseInt(getJobSeekerId()),
            type: "Put",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {
            $("#loadingIntegration").hide();
            //update the LastUpdate
            Integration.employees.loadPartial(getJobSeekerId());
            if (response) {
                DevExpress.ui.notify({ message: response.message, width: 300, shading: false }, "success", 10000);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Fail, check the log", width: 300, shading: false }, "error", 1000); });

       
    }

    this.pensionFund = function () {
        $("#loadingIntegration").show();
        $.ajax({
            url: "api/Integration/InsertUpdatePensionfund?resumeId=" + parseInt(getJobSeekerId()),
            type: "Put",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {
            //update the LastUpdate
            Integration.employees.loadPartial(getJobSeekerId());
            $("#loadingIntegration").hide();
            if (response) {
                DevExpress.ui.notify({ message: response.message, width: 300, shading: false }, "success", 10000);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Fail check the log", width: 300, shading: false }, "error", 1000); });
       
    }

    this.loadPartial = function (rid) {

        $("#integration-personalInfo-username").text('');
        $("#integration-personalInfo-lastupdate").text('');
        $("#integration-pensionInfo-username").text('');
        $("#integration-pensionInfo-lastupdate").text('');

        $.ajax({
            url: "api/Integration/GetIntegrationPersonalInfoLog?resumeId=" + parseInt(getJobSeekerId()),
            type: "Get",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {   
            
            if (response) {                
                if (response.data.length > 0) {
                    $("#integration-personalInfo-username").text(response.data[0].UserName);
                    $("#integration-personalInfo-lastupdate").text(response.data[0].LastUpdateDateTimeFormatedString);
                }              
            }
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Failed to get log", width: 300, shading: false }, "error", 1000); });

        $.ajax({
            url: "api/Integration/GetIntegrationPensionInfoLog?resumeId=" + parseInt(getJobSeekerId()),
            type: "Get",
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        }).done(function (response) {           
            if (response.data.length > 0) {
                $("#integration-pensionInfo-username").text(response.data[0].UserName);
                $("#integration-pensionInfo-lastupdate").text(response.data[0].LastUpdateDateTimeFormatedString);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) { DevExpress.ui.notify({ message: "Failed to get log", width: 300, shading: false }, "error", 1000); });
    }

    this.init = function () {        
    };
};

Integration.employees = new Integration.Employees();
$(function () {
   
    Integration.employees.init();
   
});


