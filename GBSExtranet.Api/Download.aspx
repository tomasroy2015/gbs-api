<%@ Page Language="C#" UICulture="auto" Culture="auto" %>
<%@ Import namespace="System" %>
<%@ Import namespace="System.IO" %>
<%@ Import namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<%@ Import Namespace="NPOI.HPSF" %>
<%@ Import Namespace="NPOI.HSSF.UserModel" %>

<%@ Import namespace="GBSExtranet.Api" %>

<script runat="server">
   // private static readonly ILog log = LogManager.GetLogger(typeof(SettingsTemplate_aspx));
</script>
<% 
     
    string hotelID = Request.QueryString["hotelID"];
    string culture = Request.QueryString["culture"];
    string StartDate = Request.QueryString["StartDate"];
    string Enddate = Request.QueryString["Enddate"];
    string RoomType = Request.QueryString["RoomType"];
    string PricePolicy = Request.QueryString["PricePolicy"];
    string AccommodationType = Request.QueryString["AccommodationType"];
    string WeekDay = Request.QueryString["WeekDay"];
    string fileType =   Request.QueryString["fileType"];
    string saveAsFileName = string.Empty;
   
    if(fileType == "excel")
        saveAsFileName  = "RoomRate.xls";
    if(fileType == "pdf")
        saveAsFileName = "RoomRate.pdf";
    if (fileType == "csv")
        saveAsFileName = "RoomRate.csv";
    if (fileType == "xml")
        saveAsFileName = "RoomRate.xml";
          
    Response.Clear();
    Response.Charset = "";
    Response.Cache.SetCacheability(HttpCacheability.NoCache); 
       
    if(fileType == "excel")
        Response.ContentType = "application/vnd.xls";
    if (fileType == "pdf")
        Response.ContentType = "application/pdf";
    if (fileType == "csv")
        Response.ContentType = "application/csv";
    if (fileType == "xml")
        Response.ContentType = "application/xml";
        
    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
    Download template = new Download(hotelID, culture, StartDate, Enddate, RoomType, PricePolicy, AccommodationType, WeekDay);
    //EvaluationDataTemplate oExpTemplate = new EvaluationDataTemplate(sessionID, customerID, fromDate, toDate, surveyType, serviceAreaId, isAttributeEvaluation,
    //                                                                                        attributeIdex, attributeValues, generalFilter, ticketFilter, hasTranslation, hasAdditionalRemarks, filterData);
    HSSFWorkbook hssfworkbook = template.ExportTemplate();
    
    hssfworkbook.Write(Response.OutputStream);
    Response.End();
%>

