<%@ Page Language="C#" UICulture="auto" Culture="auto" %>
<%@ Import namespace="System" %>
<%@ Import namespace="System.IO" %>
<%@ Import namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<%--<%@ Import namespace="ITsatActionWebService" %>
<%@ Import namespace="ITsatActionWebService.DAOs" %>--%>
<%--<%@ Import Namespace="log4net" %>--%>

<script runat="server">
    //private static readonly ILog log = LogManager.GetLogger(typeof(default_aspx));
</script>
<%    


    //DatasetDAO objDatasetDao = DatasetDAO.CreateInstance();    
    //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

    //string customerIPAddress = Request.ServerVariables["REMOTE_HOST"]; // retrieving the customer id
    
    //if (log.IsDebugEnabled) log.Debug("Requested ITsat Action Client from IP: " + customerIPAddress);
    
    //Company company = new CompanyDAO().GetByIpAddress(customerIPAddress);
    
    //string datasetsJson = "[]";
    //string companyJSON = "";
    
    //if (company != null)
    //{
    //    companyJSON = jsSerializer.Serialize(company).Replace("'", "\\'");

    //    IList<DatasetInfo> datasets = objDatasetDao.GetCustomerDatasets(company.ID);

    //    if (datasets.Count > 0)
    //    {
    //        // update time values to 12:00pm noon
    //        foreach (DatasetInfo dataset in datasets)
    //        {
    //            DateTime from = dataset.FromDate;
    //            DateTime to = dataset.ToDate;
    //            dataset.FromDate = new DateTime(from.Year, from.Month, from.Day, 12, 0, 0);
    //            dataset.ToDate = new DateTime(to.Year, to.Month, to.Day, 12, 0, 0);
    //        }
    //        datasetsJson = jsSerializer.Serialize(datasets).Replace("'", "\\'");
    //    }
    //}
%>
<!--#include file="Default.html"-->
