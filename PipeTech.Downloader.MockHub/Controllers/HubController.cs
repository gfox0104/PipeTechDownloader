// <copyright file="HubController.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PipeTech.Downloader.Core.Models;

namespace PipeTech.Downloader.MockHub.Controllers;

/// <summary>
/// Hub controller class.
/// </summary>
[Controller]
public class HubController : Controller
{
    /// <summary>
    /// Get the manifest uri.
    /// </summary>
    /// <param name="id">ID of the manifest to retrieve.</param>
    /// <returns>JSON of manifest information.</returns>
    [HttpGet("api/downloadManifest")]
    public IActionResult GetManifestUri([FromQuery]string id)
    {
        return this.Ok(new Uri($"http://localhost:5242/manifestfile").ToString());
        ////
        //// https://pipetech-download-manifests.s3.us-east-2.amazonaws.com/649a0cf931633f78dbec1f26.json
        ////
        ////return this.Ok(new Uri($"https://api.pipetechproject.com/manifests/{id}.json").ToString());
    }

    /// <summary>
    /// Get the manifest uri.
    /// </summary>
    /// <param name="id">ID of the manifest to retrieve.</param>
    /// <returns>JSON of manifest information.</returns>
    [HttpGet("manifestfile")]
    [HttpHead("manifestfile")]
    public IActionResult GetManifestFile()
    {
        ////try
        ////{
        ////    if (this.Request.Method.Equals("HEAD"))
        ////    {
        ////        ////this.Response.ContentLength = new FileInfo("cool.json").Length;
        ////        this.Response.ContentLength = new FileInfo("123.json").Length;
        ////        return this.Ok();
        ////    }
        ////    else
        ////    {
        ////        ////return this.PhysicalFile(Path.GetFullPath("cool.json"), "application/json");
        ////        return this.PhysicalFile(Path.GetFullPath("123.json"), "application/json");
        ////    }
        ////}
        ////catch (Exception)
        ////{
        ////    throw;
        ////}
        throw new Exception();

        ////return this.Ok(new Uri($"https://api.pipetechproject.com/manifests/{id}.json").ToString());
    }

    /// <summary>
    /// Get the manifest.
    /// </summary>
    /// <param name="id">ID of the manifest to retrieve.</param>
    /// <returns>JSON of manifest information.</returns>
    [HttpGet("manifest/{id}")]
    public ActionResult<Manifest> GetManifest(string id)
    {
        var inspectionJSON = @"
{
  ""_schemaId"": ""Model.Sewer.NASSCOv7"",
  ""_schemaVersion"": ""20.15.171.0"",
  ""$packId"": ""0481129d-431b-496a-8a0b-2a6f7ba1d248"",
  ""A_002"": [
    {
      ""upstreamAp"": ""S3004"",
      ""downstreamAp"": ""S3006"",
      ""pipeSegmentReference"": ""S3004-S3006"",
      ""owner"": ""(system owner name)"",
      ""city"": ""Fort Shafter"",
      ""street"": ""Building T320"",
      ""drainageArea"": ""DA 9"",
      ""locationCode"": ""B"",
      ""locationDetails"": ""NW corner of intersection"",
      ""pipeUse"": ""SS"",
      ""consequenceOfFailure"": null,
      ""shape"": ""C"",
      ""height"": 203.19999694824219,
      ""width"": null,
      ""material"": ""PVC"",
      ""pipeJointLength"": 6096.0,
      ""totalLength"": 35234.87890625,
      ""upRimToInvert"": 3352.800048828125,
      ""upRimToGrade"": 0.0,
      ""downRimToInvert"": 3688.080078125,
      ""downRimToGrade"": 0.0,
      ""yearConstructed"": 1974,
      ""yearRenewed"": 1997,
      ""liningMethod"": ""N"",
      ""coatingMethod"": ""XX"",
      ""mhCoordinateSystem"": ""Lat\\Lon"",
      ""verticalDatum"": ""WGS84"",
      ""gpsAccuracy"": ""N"",
      ""upNorthing"": ""21.3462075°"",
      ""upEasting"": ""-157.883424°"",
      ""upElevation"": ""70 m"",
      ""downNorthing"": ""21.3469534°"",
      ""downEasting"": ""-157.8830492°"",
      ""downElevation"": ""70 m"",
      ""comments"": null,
      ""assetType"": ""Mainline"",
      ""I_002"": [
        {
          ""upstreamAp"": ""S3004"",
          ""downstreamAp"": ""S3006"",
          ""customer"": ""(customer name)"",
          ""poNumber"": ""PO 9876"",
          ""project"": ""OWS Y3"",
          ""workOrder"": ""DA62014"",
          ""purpose"": ""D"",
          ""surveyedBy"": ""Toaalii Tauga"",
          ""certificateNumber"": ""U-1212-16905"",
          ""inspectionTechnologyUsedCctv"": true,
          ""inspectionTechnologyUsedLaser"": false,
          ""inspectionTechnologyUsedSonar"": false,
          ""inspectionTechnologyUsedSidewall"": false,
          ""inspectionTechnologyUsedZoom"": false,
          ""inspectionTechnologyUsedOther"": false,
          ""direction"": ""D"",
          ""continuation"": false,
          ""preCleaning"": ""L"",
          ""dateCleaned"": ""2013-02-11T00:00:00.0000000"",
          ""weather"": ""1"",
          ""flowControl"": ""N"",
          ""pressureValue"": null,
          ""lengthSurveyed"": 35234.87890625,
          ""inspectionTimestamp"": ""2013-02-12T08:22:00.0000000"",
          ""inspectionStatus"": ""CI"",
          ""reviewedBy"": ""MSD reviewer"",
          ""reviewerCertificateNumber"": ""U-0000-00000"",
          ""mediaLabel"": ""MainlineMedia"",
          ""comments"": ""OWS Year 3 CCTV. Deposits."",
          ""inspectionGroupID"": null,
          ""inspectionDirectoryPath"": "".\\"",
          ""iGuid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
          ""OS_002"": [
            {
              ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""osGuid"": ""31157f6f-261b-4a94-be95-6cc94cbfa0f0"",
              ""code"": ""MWL"",
              ""OF_002"": [
                {
                  ""distance"": 0.0,
                  ""code"": ""MWL"",
                  ""continuous"": null,
                  ""clock"": null,
                  ""value1stDimension"": null,
                  ""value2ndDimension"": null,
                  ""valuePercent"": 0.05,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": ""Water level approximately 5%"",
                  ""ofGuid"": ""581bb9cc-e2c5-4077-8c21-144cf2f38a55"",
                  ""osGuid"": ""31157f6f-261b-4a94-be95-6cc94cbfa0f0"",
                  ""lastModified"": ""2022-08-25T09:41:07.4142898"",
                  ""createdOn"": ""2013-02-12T08:23:01.6000000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""1230.5t"",
                  ""snapshotMediaGuid"": ""46fe2d6c-4f43-4450-8d59-f701e76e2403""
                }
              ]
            },
            {
              ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""osGuid"": ""88bb3f9e-5105-4aec-97a1-cce26a1db1bc"",
              ""code"": ""AMH"",
              ""OF_002"": [
                {
                  ""distance"": 0.0,
                  ""code"": ""AMH"",
                  ""continuous"": null,
                  ""clock"": null,
                  ""value1stDimension"": null,
                  ""value2ndDimension"": null,
                  ""valuePercent"": null,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": ""USMH: S-300-4"",
                  ""ofGuid"": ""e53e7089-8583-42bd-93db-d1dc47f92585"",
                  ""osGuid"": ""88bb3f9e-5105-4aec-97a1-cce26a1db1bc"",
                  ""lastModified"": ""2022-08-25T09:41:07.4692875"",
                  ""createdOn"": ""2013-02-12T08:22:07.1670000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""C787-6D0A-4832-B302-EDA0-B312-9520-3E7E-D96F-0282-50AF-9A88"",
                  ""snapshotMediaGuid"": ""d47af611-0e94-45dc-a333-ef8d72e3e569""
                }
              ]
            },
            {
              ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""osGuid"": ""247473c6-ab8b-4607-aa96-cdbc74f1fb62"",
              ""code"": ""MMC"",
              ""OF_002"": [
                {
                  ""distance"": 15880.08,
                  ""code"": ""MMC"",
                  ""continuous"": null,
                  ""clock"": null,
                  ""value1stDimension"": null,
                  ""value2ndDimension"": null,
                  ""valuePercent"": null,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": ""Pipe material appears to be PVC"",
                  ""ofGuid"": ""247473c6-ab8b-4607-aa96-cdbc74f1fb62"",
                  ""osGuid"": ""247473c6-ab8b-4607-aa96-cdbc74f1fb62"",
                  ""lastModified"": ""2022-08-25T09:41:07.5323203"",
                  ""createdOn"": ""2013-02-12T08:26:56.4330000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""9E5C-E47B-1332-4B04-21D2-C3EA-C4D2-D30D-D96F-0282-50AF-9A88"",
                  ""snapshotMediaGuid"": ""3ed34cea-1043-4eb1-a898-d6a8dd143ce3""
                }
              ]
            },
            {
              ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""osGuid"": ""bcaeccb0-32e7-4802-9d8c-85e6075cba7a"",
              ""code"": ""DAGS"",
              ""OF_002"": [
                {
                  ""distance"": 1219.2,
                  ""code"": ""DAGS"",
                  ""continuous"": ""S01"",
                  ""clock"": {
                    ""from"": 5,
                    ""to"": 7
                  },
                  ""value1stDimension"": null,
                  ""value2ndDimension"": null,
                  ""valuePercent"": 0.05,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": null,
                  ""ofGuid"": ""39a158c1-3422-4deb-abb3-e19a0f2fa32e"",
                  ""osGuid"": ""bcaeccb0-32e7-4802-9d8c-85e6075cba7a"",
                  ""lastModified"": ""2022-08-25T09:41:07.5853196"",
                  ""createdOn"": ""2013-02-12T08:23:08.6670000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""2D14-3025-0AB5-BC29-EDA0-B312-9520-3E7E-D96F-0282-50AF-9A88""
                },
                {
                  ""distance"": 35052.0,
                  ""code"": ""DAGS"",
                  ""continuous"": ""F01"",
                  ""clock"": {
                    ""from"": 5,
                    ""to"": 7
                  },
                  ""value1stDimension"": null,
                  ""value2ndDimension"": null,
                  ""valuePercent"": 0.05,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": null,
                  ""ofGuid"": ""b9d9f47c-3166-4694-a2c9-316d0df4bcfd"",
                  ""osGuid"": ""bcaeccb0-32e7-4802-9d8c-85e6075cba7a"",
                  ""lastModified"": ""2022-08-25T09:41:07.6073219"",
                  ""createdOn"": ""2013-02-12T08:31:57.0000000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""4B3E-E195-3964-52BA""
                }
              ]
            },
            {
              ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""osGuid"": ""4be3214a-4cdb-4c4e-bcbb-86b962d4d9a3"",
              ""code"": ""AMH"",
              ""OF_002"": [
                {
                  ""distance"": 35234.88,
                  ""code"": ""AMH"",
                  ""continuous"": null,
                  ""clock"": null,
                  ""value1stDimension"": null,
                  ""value2ndDimension"": null,
                  ""valuePercent"": null,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": ""DSMH: S-300-6"",
                  ""ofGuid"": ""ebbc9bf7-7f4c-47d3-bb88-b2d36f960968"",
                  ""osGuid"": ""4be3214a-4cdb-4c4e-bcbb-86b962d4d9a3"",
                  ""lastModified"": ""2022-08-25T09:41:07.6632903"",
                  ""createdOn"": ""2013-02-12T08:32:26.5670000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""150F-38C8-2127-441C-EDA0-B312-9520-3E7E-D96F-0282-50AF-9A88""
                }
              ]
            },
            {
              ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""osGuid"": ""9c441617-a1af-47ad-bba3-28f65f76bed7"",
              ""code"": ""TFA"",
              ""OF_002"": [
                {
                  ""distance"": 23957.28,
                  ""code"": ""TFA"",
                  ""continuous"": null,
                  ""clock"": {
                    ""from"": 5,
                    ""to"": null
                  },
                  ""value1stDimension"": 152.4,
                  ""value2ndDimension"": null,
                  ""valuePercent"": null,
                  ""joint"": false,
                  ""Severity"": null,
                  ""grade"": null,
                  ""comments"": null,
                  ""ofGuid"": ""609677d2-9ed5-4d1f-986c-0fa1c959988f"",
                  ""osGuid"": ""9c441617-a1af-47ad-bba3-28f65f76bed7"",
                  ""lastModified"": ""2022-08-25T09:41:07.7262904"",
                  ""createdOn"": ""2013-02-12T08:29:42.6670000"",
                  ""i002Guid"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
                  ""mediaGuid"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
                  ""mediaParameters"": ""A06A-2BAB-EF04-7096-EDA0-B312-9520-3E7E-D96F-0282-50AF-9A88""
                }
              ]
            }
          ],
          ""Media_002"": [
            {
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Media_URI"": ""https://media.pipetechproject.com/6342c0f37056fd7b942241a3/inspections/5b2a22c3-11c7-466f-82f0-eff1a9025ff0/230606%20(mh101--mh102)%201932.mp4"",
              ""Length"": 260006387,
              ""Hash"": null,
              ""Media_Info"": null,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Embedded_Overlay"": 31
            },
            {
              ""Media_GUID"": ""46fe2d6c-4f43-4450-8d59-f701e76e2403"",
              ""Media_URI"": ""https://media.pipetechproject.com/6342c0f37056fd7b942241a3/inspections/5b2a22c3-11c7-466f-82f0-eff1a9025ff0/000.0%20ft%20-%20AMH.jpg"",
              ""Length"": 23409,
              ""Hash"": null,
              ""Media_Info"": null,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Embedded_Overlay"": null
            },
            {
              ""Media_GUID"": ""d47af611-0e94-45dc-a333-ef8d72e3e569"",
              ""Media_URI"": ""https://media.pipetechproject.com/6342c0f37056fd7b942241a3/inspections/5b2a22c3-11c7-466f-82f0-eff1a9025ff0/000.0 ft - MWL.jpg"",
              ""Length"": 28159,
              ""Hash"": null,
              ""Media_Info"": null,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Embedded_Overlay"": null
            },
            {
              ""Media_GUID"": ""3ed34cea-1043-4eb1-a898-d6a8dd143ce3"",
              ""Media_URI"": ""https://media.pipetechproject.com/6342c0f37056fd7b942241a3/inspections/5b2a22c3-11c7-466f-82f0-eff1a9025ff0/100.8 ft - AMH.jpg"",
              ""Length"": 23623,
              ""Hash"": null,
              ""Media_Info"": null,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Embedded_Overlay"": null
            }
          ],
          ""Attachments_002"": [],
          ""Functions_of_Distance_002"": [],
          ""Functions_of_MediaTime_002"": [
            {
              ""Measurement_GUID"": ""9ae68594-ba04-4883-9a6e-c3428da231db"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 0.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1219.2
            },
            {
              ""Measurement_GUID"": ""d1c482cf-ffdb-425e-96a8-9187c09c8df2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 84751.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1249.68
            },
            {
              ""Measurement_GUID"": ""78516d8e-4925-4a9c-a9d6-a61a22f4ce48"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 84885.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1280.16
            },
            {
              ""Measurement_GUID"": ""fdc41de9-ea42-4faf-af26-177f10ceb557"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 85219.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1310.64
            },
            {
              ""Measurement_GUID"": ""04ac29fa-7b4c-48fb-84ee-f22747eed33e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 85919.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1341.12
            },
            {
              ""Measurement_GUID"": ""79bacfeb-6344-45a1-87fc-c82aa90385b7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 104204.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1889.76
            },
            {
              ""Measurement_GUID"": ""b2d82b30-c6ce-4f58-9abc-934539f2b101"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 115249.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1920.24
            },
            {
              ""Measurement_GUID"": ""1fa0d166-5a2e-4fdb-89c6-82021c449196"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 116049.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1950.72
            },
            {
              ""Measurement_GUID"": ""6380db83-dcf9-4632-a816-83339ad10770"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 118051.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 1981.2
            },
            {
              ""Measurement_GUID"": ""4fb78c1c-0b66-4aa5-a4dc-d806a89816c7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 118118.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2011.68
            },
            {
              ""Measurement_GUID"": ""4c73b486-63cb-4c58-817d-7bcb930fcabc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 118285.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2042.16
            },
            {
              ""Measurement_GUID"": ""4c17b146-8cfa-4fa0-b931-5ac0b8387df1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 118519.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2072.64
            },
            {
              ""Measurement_GUID"": ""3c9d3781-8f8f-4203-b553-581f2567f612"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 118619.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2103.12
            },
            {
              ""Measurement_GUID"": ""b264446e-1d69-4d0d-8cd0-d73753e6f322"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 118785.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2133.6
            },
            {
              ""Measurement_GUID"": ""ad7c8871-2717-416c-8103-1f7e2c14b1d9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 119653.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2164.08
            },
            {
              ""Measurement_GUID"": ""968d001d-cb6b-48a5-973f-6c2cfdf32ed9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 120721.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2194.56
            },
            {
              ""Measurement_GUID"": ""f3b76d0e-d5fc-413f-8722-b4d41ae75330"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 120821.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2225.04
            },
            {
              ""Measurement_GUID"": ""99cc5600-7e88-4f19-9eb2-67515a6664dc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 120888.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2255.52
            },
            {
              ""Measurement_GUID"": ""7cc57b4a-7fe8-40d5-a154-5fb0dbe5175c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 121121.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2286.0
            },
            {
              ""Measurement_GUID"": ""bd0fb8e5-4004-4262-992c-64f84068a50a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 121188.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2316.48
            },
            {
              ""Measurement_GUID"": ""bcc329f5-7103-42dc-abf3-b1becfbb1cf2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 121321.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2346.96
            },
            {
              ""Measurement_GUID"": ""a9048590-6357-4b4c-87d4-3299b0c6b44a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 128095.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2377.44
            },
            {
              ""Measurement_GUID"": ""2ea52e20-a1e5-4adb-99c6-a9f39e22b1bb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 121588.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2407.92
            },
            {
              ""Measurement_GUID"": ""636e31e1-393d-4345-b69c-35ca8b07a082"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 122122.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2438.4
            },
            {
              ""Measurement_GUID"": ""31ce5e5e-428b-4723-8803-24a3f97a67e1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 123223.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2468.88
            },
            {
              ""Measurement_GUID"": ""56d854d9-bd63-4775-8457-9cf9a715a7ac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 123290.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2499.36
            },
            {
              ""Measurement_GUID"": ""d895ccf7-345a-42b4-b5ef-fa11162dfeff"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 123390.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2529.84
            },
            {
              ""Measurement_GUID"": ""a6158360-6524-4d0b-8111-7f3247c63084"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 123724.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2560.32
            },
            {
              ""Measurement_GUID"": ""7f1f7098-c787-48b2-9372-3493d2fd66dc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 123991.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2590.8
            },
            {
              ""Measurement_GUID"": ""23f47997-4092-446a-9621-dc8f16655a71"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 124091.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2621.28
            },
            {
              ""Measurement_GUID"": ""a2fa1088-1489-4269-9e47-997d5d15274f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 124424.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2651.76
            },
            {
              ""Measurement_GUID"": ""540a3251-19d7-4364-bcf7-b21214dfb980"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 124858.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2682.24
            },
            {
              ""Measurement_GUID"": ""aa8ec879-3d2f-49ba-bc11-62e78b099cf1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 124958.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2712.72
            },
            {
              ""Measurement_GUID"": ""97c9d7c0-c9b2-4f07-838f-d196a7eb1733"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125025.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2743.2
            },
            {
              ""Measurement_GUID"": ""0c00af27-6c0a-4274-807c-1f55d1c2901f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125158.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2773.68
            },
            {
              ""Measurement_GUID"": ""d04e27e6-6661-4f44-8d91-03c8a57155d8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125259.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2804.16
            },
            {
              ""Measurement_GUID"": ""de45a2e9-e048-4229-ad95-3abcdb78ca38"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125359.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2834.64
            },
            {
              ""Measurement_GUID"": ""9d6e920c-e68b-4cd3-bbac-5b62fa31cfcf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125492.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2865.12
            },
            {
              ""Measurement_GUID"": ""1a986753-83ed-4385-baff-9923754cda61"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125559.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2895.6
            },
            {
              ""Measurement_GUID"": ""b75f0f19-7396-4a6e-ac77-93294825bad9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125692.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2926.08
            },
            {
              ""Measurement_GUID"": ""249cb0e7-ece3-4241-9d05-14f12395b46e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125826.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2956.56
            },
            {
              ""Measurement_GUID"": ""b80631f6-41bb-420a-9ff8-062cb48d7325"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125859.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 2987.04
            },
            {
              ""Measurement_GUID"": ""14c0592d-f1cf-4aa6-a8f5-f91452e65b9f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 125926.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3017.52
            },
            {
              ""Measurement_GUID"": ""3a31ca46-a62c-4977-b79a-b7b1ecda3156"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 126059.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3048.0
            },
            {
              ""Measurement_GUID"": ""fef0d26a-7160-4bf9-ac45-9a570792e249"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 126159.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3078.48
            },
            {
              ""Measurement_GUID"": ""137a2a08-e756-4f4f-aa4d-82142818bdc5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 126226.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3108.96
            },
            {
              ""Measurement_GUID"": ""698adcbc-757e-4c09-83c1-c2f39f0a17bc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 126326.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3139.44
            },
            {
              ""Measurement_GUID"": ""69b92f6b-11dc-4a4d-9097-67c730e124f4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 126460.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3169.92
            },
            {
              ""Measurement_GUID"": ""c1a0516f-167a-4292-b657-3747df7daf15"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 126627.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3200.4
            },
            {
              ""Measurement_GUID"": ""476d4d1b-2042-4e1d-bbac-540ac735f1d6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 127227.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3230.88
            },
            {
              ""Measurement_GUID"": ""8f210d86-feb2-4c11-b5ce-bb0baa3c8ebe"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 132766.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3261.36
            },
            {
              ""Measurement_GUID"": ""28cbecb9-3219-4dc2-ad17-8b3768bbbf4d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 132833.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3291.84
            },
            {
              ""Measurement_GUID"": ""031dd354-aa6f-4038-8caf-85d3b5beba48"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 132966.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3322.32
            },
            {
              ""Measurement_GUID"": ""29365690-4aea-43e4-91fe-e7c7df8f9e90"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 133233.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3352.8
            },
            {
              ""Measurement_GUID"": ""8e90bc9e-b9ef-40d3-bd5d-a0a013b98cc2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 133400.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3383.28
            },
            {
              ""Measurement_GUID"": ""399afdf9-1d5d-4b26-bc77-cb484537cde7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 133534.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3413.76
            },
            {
              ""Measurement_GUID"": ""333ba351-17ba-44ac-8a2e-bec68a4c351c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 133800.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3444.24
            },
            {
              ""Measurement_GUID"": ""87340d73-5449-40b9-978d-f20769a3f376"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 134234.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3474.72
            },
            {
              ""Measurement_GUID"": ""1fc0f25f-70d0-490e-9427-986d1675b568"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 134434.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3505.2
            },
            {
              ""Measurement_GUID"": ""ec8eae7e-3e3f-4cf0-98f0-64821f445b6a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 135135.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3535.68
            },
            {
              ""Measurement_GUID"": ""32e9e1e8-28e4-4e97-b384-19243e778fe8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 135202.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3566.16
            },
            {
              ""Measurement_GUID"": ""112f464f-6560-41da-8dae-07cf7213b0cc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 136203.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3596.64
            },
            {
              ""Measurement_GUID"": ""6cb28872-7d60-4e60-bda0-f8705a0a56d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 138205.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3627.12
            },
            {
              ""Measurement_GUID"": ""473c708e-4c01-4437-b5d2-96f6a48d59d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 139173.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3657.6
            },
            {
              ""Measurement_GUID"": ""08853bd4-0a1c-475e-b374-15f75825fcdb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 140474.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3688.08
            },
            {
              ""Measurement_GUID"": ""9f83663b-360d-4e61-aa68-49447b1a2c82"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 141642.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3718.56
            },
            {
              ""Measurement_GUID"": ""d6399304-202a-4179-8650-a9cb21e67cf9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 142042.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3749.04
            },
            {
              ""Measurement_GUID"": ""511d407e-42c9-4b10-b9e4-18f169b5813b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 142576.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3779.52
            },
            {
              ""Measurement_GUID"": ""fe2d131c-3f98-4c93-8877-34d6d7a2976f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 143477.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3810.0
            },
            {
              ""Measurement_GUID"": ""98f40eab-0a0d-4798-a879-6bd8d76c684b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 143911.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3840.48
            },
            {
              ""Measurement_GUID"": ""bb63997c-a329-46b7-8b1e-938ca07e94be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 144344.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3870.96
            },
            {
              ""Measurement_GUID"": ""47529d61-0f6a-4c71-b8e7-0c56bbd4324f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 144878.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3901.44
            },
            {
              ""Measurement_GUID"": ""499e4968-256d-4a95-90d5-799787e1bb87"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 145312.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3931.92
            },
            {
              ""Measurement_GUID"": ""7d6192b4-ed0d-4cdf-92e2-7cef649877c4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 145646.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3962.4
            },
            {
              ""Measurement_GUID"": ""4fc0feaa-e85e-438a-8658-59fc880830f5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 146046.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 3992.88
            },
            {
              ""Measurement_GUID"": ""315533f7-1687-4ffd-9a99-ed6d32c0f8ec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 146280.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4023.36
            },
            {
              ""Measurement_GUID"": ""9ba7a0d1-ef91-4738-b8d1-55260ac69345"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 146947.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4053.84
            },
            {
              ""Measurement_GUID"": ""a22f442c-b6de-4998-8b82-03b1d3fe59eb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 147481.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4084.32
            },
            {
              ""Measurement_GUID"": ""7edf983b-292d-4077-b7f4-5e31acd47369"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 148215.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4114.8
            },
            {
              ""Measurement_GUID"": ""f758ef1d-ed55-4f6d-b875-a6f03d08dc48"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 148815.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4145.28
            },
            {
              ""Measurement_GUID"": ""8579e021-a8c0-4467-8e61-db773b526ca8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 149283.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4175.76
            },
            {
              ""Measurement_GUID"": ""56feb54a-1638-4b8e-ad75-50e403d25fed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 149449.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4206.24
            },
            {
              ""Measurement_GUID"": ""60480caa-75b9-4fa7-b1a4-dd645d375b86"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 149616.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4236.72
            },
            {
              ""Measurement_GUID"": ""0535bf7c-4124-4da5-8346-aa8a62fd0d32"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 150384.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4267.2
            },
            {
              ""Measurement_GUID"": ""1bcbc9f2-6120-4d62-834a-1a7bf768c33a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 151885.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4297.68
            },
            {
              ""Measurement_GUID"": ""88376733-05f0-4552-bbeb-5b682c5306c9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 151151.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4328.16
            },
            {
              ""Measurement_GUID"": ""f10820ac-1f70-4ac2-a917-5ee8bc7c2b5d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 151418.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4358.64
            },
            {
              ""Measurement_GUID"": ""b723be1b-84ce-487d-8d86-06d412a5d52e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 151718.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4389.12
            },
            {
              ""Measurement_GUID"": ""991d8ccf-79c6-4975-af40-06c6cfb7f1c3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 152286.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4419.6
            },
            {
              ""Measurement_GUID"": ""525b186b-364b-4af3-9026-38e2cbdf3c49"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 152786.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4450.08
            },
            {
              ""Measurement_GUID"": ""b232239d-1bd3-489b-b17f-82c044d01f14"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 153086.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4480.56
            },
            {
              ""Measurement_GUID"": ""19a97b49-c599-46e7-b297-b60535dd7e10"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 153320.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4511.04
            },
            {
              ""Measurement_GUID"": ""b191efc7-cbe1-47d6-8429-a32d860985f0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 153954.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4541.52
            },
            {
              ""Measurement_GUID"": ""cd502722-3664-46d2-885f-706ac2e520ff"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 154288.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4572.0
            },
            {
              ""Measurement_GUID"": ""262e5f68-dc02-4d4e-b8f1-2a3077685ded"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 154555.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4602.48
            },
            {
              ""Measurement_GUID"": ""91515818-5455-4529-8930-547984d8929f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 154955.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4632.96
            },
            {
              ""Measurement_GUID"": ""ad2e68f9-ce81-4ca7-80f0-fbfafba43580"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 155289.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4663.44
            },
            {
              ""Measurement_GUID"": ""81a3508f-6240-44d3-a101-4c4f92f6cab4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 155722.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4693.92
            },
            {
              ""Measurement_GUID"": ""b4239dd6-2d65-4dc2-ab11-5ba1b75d485a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 156089.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4724.4
            },
            {
              ""Measurement_GUID"": ""56cd419b-b12a-475a-906f-e137a11b7092"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 156456.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4754.88
            },
            {
              ""Measurement_GUID"": ""46a2f28c-1124-477a-9a7a-62c8759f58e6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 156924.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4785.36
            },
            {
              ""Measurement_GUID"": ""8b63ee47-1086-4b57-8596-59f8611e45da"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 157291.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4815.84
            },
            {
              ""Measurement_GUID"": ""fb76e957-ac5a-46aa-bd7f-d8b58eb2483e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 157891.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4846.32
            },
            {
              ""Measurement_GUID"": ""ba9bb7df-7b4e-4299-87c4-45e957238301"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 158292.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4876.8
            },
            {
              ""Measurement_GUID"": ""6e94801e-2bcb-47b2-9bfb-b4c5bdc1c0f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 158525.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4907.28
            },
            {
              ""Measurement_GUID"": ""7eb1d2f2-6752-438a-a649-3080037a4c42"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 159092.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4937.76
            },
            {
              ""Measurement_GUID"": ""ffb66b46-31a7-4717-96f4-b291d9ad0cd1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 159526.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4968.24
            },
            {
              ""Measurement_GUID"": ""1a1b88e7-a0da-4abe-b8eb-7aec45977349"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 159893.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 4998.72
            },
            {
              ""Measurement_GUID"": ""12d0441f-491a-4dd5-8969-a62c45ea4a30"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 160093.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5029.2
            },
            {
              ""Measurement_GUID"": ""1d54a82a-cf85-45c0-a036-d97583303291"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 160894.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5059.68
            },
            {
              ""Measurement_GUID"": ""2a7633e4-0592-406e-9a9d-a2ed4f85be08"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 161128.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5090.16
            },
            {
              ""Measurement_GUID"": ""c3ca6425-7d82-487b-a71e-ed544ac87668"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 161361.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5120.64
            },
            {
              ""Measurement_GUID"": ""add651ae-fea6-4b96-b7e6-19016775e27d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 161562.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5151.12
            },
            {
              ""Measurement_GUID"": ""4e535ea1-c04a-4ab9-91bd-0330bea24e60"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 161695.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5181.6
            },
            {
              ""Measurement_GUID"": ""14c829d0-09af-4295-8adb-324c8f88153b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 161895.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5212.08
            },
            {
              ""Measurement_GUID"": ""3879f223-6bfd-4f04-aeff-e142ce391cbd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162029.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5242.56
            },
            {
              ""Measurement_GUID"": ""549ddc4f-905b-4797-a9f4-625640657c88"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162095.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5273.04
            },
            {
              ""Measurement_GUID"": ""f4992afe-e93e-4e85-9304-c2b73062f2cd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162196.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5303.52
            },
            {
              ""Measurement_GUID"": ""e087ec4e-04a3-49c1-af8a-ad92b87bbd73"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162296.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5334.0
            },
            {
              ""Measurement_GUID"": ""1e9a00f9-fb49-497f-9f78-bc179dc64fd5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162429.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5364.48
            },
            {
              ""Measurement_GUID"": ""fe862920-e87a-4076-afd0-ba667ccf2976"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162563.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5394.96
            },
            {
              ""Measurement_GUID"": ""b07e0803-981b-4f73-b91f-36c3aed57aa3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162696.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5425.44
            },
            {
              ""Measurement_GUID"": ""d9bb4feb-9572-46db-a80f-9721dfd920ce"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162796.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5455.92
            },
            {
              ""Measurement_GUID"": ""34b5123c-8124-41fb-9242-1745c377d66e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 162963.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5486.4
            },
            {
              ""Measurement_GUID"": ""645cf0a2-aaef-478c-8bbf-cf0b8c5a916c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 163430.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5516.88
            },
            {
              ""Measurement_GUID"": ""2205ae5d-ef0a-4509-a8c4-0cae1afcba26"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 163630.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5547.36
            },
            {
              ""Measurement_GUID"": ""abe4b85f-b946-488a-acdb-c5d3ba3ab571"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 163931.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5577.84
            },
            {
              ""Measurement_GUID"": ""d7ec1ce5-4575-4cb9-a4f7-2a5ce6cb4cca"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 164097.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5608.32
            },
            {
              ""Measurement_GUID"": ""dfe18d82-3b1f-4af1-a2d4-514390c975f4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 164198.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5638.8
            },
            {
              ""Measurement_GUID"": ""fd7d871c-f670-4557-82ae-69b8ccf54dec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 164398.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5669.28
            },
            {
              ""Measurement_GUID"": ""9c923ea2-83e3-45b2-80d4-20220ef10989"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 164631.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5699.76
            },
            {
              ""Measurement_GUID"": ""51be3bd1-78e1-438c-b56a-a448c3e269fa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 164765.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5730.24
            },
            {
              ""Measurement_GUID"": ""30f4c916-d01f-406e-ac35-3659c8fe113c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 164898.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5760.72
            },
            {
              ""Measurement_GUID"": ""34073716-b8c7-429e-9004-f684958f60c0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 165032.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5791.2
            },
            {
              ""Measurement_GUID"": ""753ca7aa-5066-46a4-a7db-bb6c99778e0e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 165265.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5821.68
            },
            {
              ""Measurement_GUID"": ""62c4ceab-9470-42cf-908b-53a0e04dca91"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 165465.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5852.16
            },
            {
              ""Measurement_GUID"": ""ccc98afb-5e36-4c9e-ab60-bf52554c1df3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 165599.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5882.64
            },
            {
              ""Measurement_GUID"": ""73d130b0-8902-4f5e-86d7-dbdaa1d80fd6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 165799.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5913.12
            },
            {
              ""Measurement_GUID"": ""49d33a68-f4a6-4e2d-9af4-72bc9e19f169"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 165933.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5943.6
            },
            {
              ""Measurement_GUID"": ""d694bb9a-5a97-41f4-9c22-b0a0663c59fc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 166266.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 5974.08
            },
            {
              ""Measurement_GUID"": ""790fbaf6-0bd6-49e8-8a56-4766bdec782e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 166500.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6004.56
            },
            {
              ""Measurement_GUID"": ""ef0c5430-5c93-48f7-96aa-4739a8afca06"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 167000.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6035.04
            },
            {
              ""Measurement_GUID"": ""49e1cb06-d349-4363-82ef-c96861487bb3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 167835.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6065.52
            },
            {
              ""Measurement_GUID"": ""2bbd4844-ae01-41f8-89f3-5fa3385e3e48"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 168569.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6096.0
            },
            {
              ""Measurement_GUID"": ""f15ecb99-90b5-4e6e-94ff-3a6aa94ec9a8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 169403.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6126.48
            },
            {
              ""Measurement_GUID"": ""d35f3535-5d0d-469c-956f-91aa1bc2d51f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 169670.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6156.96
            },
            {
              ""Measurement_GUID"": ""4f5cfe75-6a1c-4b94-adce-42016200984d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 169837.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6187.44
            },
            {
              ""Measurement_GUID"": ""8e39230b-15f7-4507-8ebf-1df2e41aeb92"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 170070.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6217.92
            },
            {
              ""Measurement_GUID"": ""d34efa61-e6c6-4efd-8f4b-8c8b6de094bd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 170537.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6248.4
            },
            {
              ""Measurement_GUID"": ""190ed194-6922-457f-a5fc-4450c2cab2fe"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 171038.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6278.88
            },
            {
              ""Measurement_GUID"": ""0191518f-538d-428e-8671-cc9b93ac67b1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 171271.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6309.36
            },
            {
              ""Measurement_GUID"": ""fcc7bade-a42d-4030-8b80-3a9016cf91ad"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 171672.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6339.84
            },
            {
              ""Measurement_GUID"": ""e043389f-fda6-49c2-8eba-cc175b4f9e7c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 171972.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6370.32
            },
            {
              ""Measurement_GUID"": ""bf562611-3612-4c8b-8d45-d0d92ab090f9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 172439.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6400.8
            },
            {
              ""Measurement_GUID"": ""9827a1c9-b7b9-4be4-8883-73d6c06b0791"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 172806.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6431.28
            },
            {
              ""Measurement_GUID"": ""640499de-5f31-46e4-8d51-10ea0e4e9465"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 173006.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6461.76
            },
            {
              ""Measurement_GUID"": ""49178407-0ba7-44da-be96-8a810400a6d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 173507.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6492.24
            },
            {
              ""Measurement_GUID"": ""e47aa05b-049c-4371-a9a1-eeb3ae1aad65"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 174274.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6522.72
            },
            {
              ""Measurement_GUID"": ""30eaaca1-a9b7-4ac1-a635-f67c89ece440"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 174575.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6553.2
            },
            {
              ""Measurement_GUID"": ""dcb576be-b179-4701-a7fb-85c056732ef3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 174775.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6583.68
            },
            {
              ""Measurement_GUID"": ""dadd83a9-7740-43fd-9a90-152ce5603ca2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 175242.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6614.16
            },
            {
              ""Measurement_GUID"": ""e3330d23-7770-4d2a-84c7-d4b581abbe11"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 175709.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6644.64
            },
            {
              ""Measurement_GUID"": ""2159f077-2a28-4539-8d3e-e8ea9828ca7b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 176243.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6675.12
            },
            {
              ""Measurement_GUID"": ""960c34a5-5684-4104-bbf4-d1653c2dd549"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 176543.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6705.6
            },
            {
              ""Measurement_GUID"": ""1f0d9ba8-88bc-49c2-b9b3-cfd53a56c294"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 176777.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6736.08
            },
            {
              ""Measurement_GUID"": ""1e125a75-511a-4626-85e4-28f5324db91e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 177077.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6766.56
            },
            {
              ""Measurement_GUID"": ""35f1e179-5a82-4cef-8968-634d115b19d2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 177611.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6797.04
            },
            {
              ""Measurement_GUID"": ""76fef27a-daa6-4706-98cc-2b5d41ad568b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 178078.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6827.52
            },
            {
              ""Measurement_GUID"": ""64ffa08b-1555-4ae9-91f1-466235da5bc7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 178312.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6858.0
            },
            {
              ""Measurement_GUID"": ""8263343b-ee63-4b95-a090-33379bc8fb71"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 178879.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6888.48
            },
            {
              ""Measurement_GUID"": ""09f68a2d-2960-437e-ab82-c54560a9662d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 179279.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6918.96
            },
            {
              ""Measurement_GUID"": ""5ca70781-dffe-4e0d-ba05-86d0fda6e00d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 179546.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6949.44
            },
            {
              ""Measurement_GUID"": ""cf906729-9473-4f08-a51c-3b54f8a60ed3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 180047.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 6979.92
            },
            {
              ""Measurement_GUID"": ""f812b7c9-a5a2-4013-9e70-2fbc50df7712"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 180614.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7010.4
            },
            {
              ""Measurement_GUID"": ""bea9c143-20b2-46f0-a405-a331de4db757"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 180814.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7040.88
            },
            {
              ""Measurement_GUID"": ""0067f481-c838-4ab9-b239-b6968b21f7c9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 181148.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7071.36
            },
            {
              ""Measurement_GUID"": ""5247b5eb-6e47-40b5-a0a7-5823a147ad01"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 181615.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7101.84
            },
            {
              ""Measurement_GUID"": ""0beee6cf-c24e-4551-8350-68c034d8181d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 181849.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7132.32
            },
            {
              ""Measurement_GUID"": ""067b7f3a-4962-4cf2-a2d3-79514842d4d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 182115.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7162.8
            },
            {
              ""Measurement_GUID"": ""e463b7bf-3b30-4c49-8680-989da7715709"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 182683.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7193.28
            },
            {
              ""Measurement_GUID"": ""87d44cab-0649-431d-8756-6339ceb4b2ca"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 183116.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7223.76
            },
            {
              ""Measurement_GUID"": ""ff040096-4d13-494b-9380-c9d5e3743305"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 183550.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7254.24
            },
            {
              ""Measurement_GUID"": ""f6c98c4d-94ec-43f0-ad4c-580407b766e4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 183951.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7284.72
            },
            {
              ""Measurement_GUID"": ""2559ece4-1fb4-455d-8b44-177ccbc5f278"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 184318.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7315.2
            },
            {
              ""Measurement_GUID"": ""27358672-ba01-41b6-8d8a-6b7e468be426"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 184751.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7345.68
            },
            {
              ""Measurement_GUID"": ""d838f8c1-63e0-41eb-beeb-a7d817ff0b47"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 184985.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7376.16
            },
            {
              ""Measurement_GUID"": ""4bf2ea2f-ba3f-4397-8db6-881a252ee4b0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 185252.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7406.64
            },
            {
              ""Measurement_GUID"": ""6034e8c1-44d4-40e0-a0d2-d3a1c79213c9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 185719.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7437.12
            },
            {
              ""Measurement_GUID"": ""927dbe45-6565-4898-a004-cdcd14d6668a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 186286.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7467.6
            },
            {
              ""Measurement_GUID"": ""eefc124a-aa18-427e-9951-5937dca83179"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 186486.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7498.08
            },
            {
              ""Measurement_GUID"": ""a53e0baa-75a3-483c-8154-2efa6f21fa23"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 186687.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7528.56
            },
            {
              ""Measurement_GUID"": ""c25b195b-ffb4-4d78-8f6c-de3370ba6644"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 187254.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7559.04
            },
            {
              ""Measurement_GUID"": ""73cab53a-9ea7-4e32-a621-2b5284019070"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 187688.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7589.52
            },
            {
              ""Measurement_GUID"": ""521b9ed6-52e4-4b0e-a02d-7c4a2ea44dc3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 187955.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7620.0
            },
            {
              ""Measurement_GUID"": ""01b43010-aed0-458a-8f26-9875a5b0292a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 188288.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7650.48
            },
            {
              ""Measurement_GUID"": ""4589af31-cead-4218-bc45-7082d6f81eee"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 188555.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7680.96
            },
            {
              ""Measurement_GUID"": ""dbb11104-ee9e-492c-8676-143a1b9b99e6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 188755.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7711.44
            },
            {
              ""Measurement_GUID"": ""d98e178e-fbbf-4271-a02d-dedc77ad5aa6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 189489.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7741.92
            },
            {
              ""Measurement_GUID"": ""bde52c70-1df6-4219-bb00-872f5fabc137"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 189990.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7772.4
            },
            {
              ""Measurement_GUID"": ""c709341b-bd18-4373-91b0-790151726415"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 190224.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7802.88
            },
            {
              ""Measurement_GUID"": ""9cfafef4-0e0d-4923-bce0-38bc4fb0d140"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 190457.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7833.36
            },
            {
              ""Measurement_GUID"": ""507bb47f-8cb0-43c7-8542-e5745580ef55"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 190824.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7863.84
            },
            {
              ""Measurement_GUID"": ""adfad166-a344-4931-9fd1-eea34dbf6596"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 191091.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7894.32
            },
            {
              ""Measurement_GUID"": ""fd9ccbeb-2af8-4a87-9886-8e86fe93e9ce"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 191725.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7924.8
            },
            {
              ""Measurement_GUID"": ""341d9671-0c0c-4239-9c95-547696941640"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 192226.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7955.28
            },
            {
              ""Measurement_GUID"": ""e82bf692-435c-4698-9012-08fd3964a251"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 192526.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 7985.76
            },
            {
              ""Measurement_GUID"": ""56e13629-3107-4a75-bd8a-8b039e18dbd6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 192726.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8016.24
            },
            {
              ""Measurement_GUID"": ""e5d80e03-98ea-438f-aaf6-eb25afe7b907"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 193093.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8046.72
            },
            {
              ""Measurement_GUID"": ""ef84ddbb-111d-47d4-91a3-23aa331ae481"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 193393.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8077.2
            },
            {
              ""Measurement_GUID"": ""55900de8-687b-4ec6-af79-6772067d5387"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 193927.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8107.68
            },
            {
              ""Measurement_GUID"": ""b234c5cc-f92f-4e98-8929-44a35c2874d2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 194561.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8138.16
            },
            {
              ""Measurement_GUID"": ""85f585be-6c52-480c-b5f0-3b29465ad5e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 194862.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8168.64
            },
            {
              ""Measurement_GUID"": ""6902d500-5ff6-474b-a5ce-3842507a8eef"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 195062.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8199.12
            },
            {
              ""Measurement_GUID"": ""f1f681ef-c3c5-4998-ae8e-1c3e58afdce6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 195362.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8229.6
            },
            {
              ""Measurement_GUID"": ""9840ad8f-e769-4fd9-b307-eb383235be8f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 195796.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8260.08
            },
            {
              ""Measurement_GUID"": ""4433d389-2adb-41a6-99ea-f69d2d061462"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 196263.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8290.56
            },
            {
              ""Measurement_GUID"": ""218fb8d8-a0c4-46a1-ba02-0d802d7e455f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 196663.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8321.04
            },
            {
              ""Measurement_GUID"": ""43dcbee7-a56e-42cd-a0a8-9b66fa5abef3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 196897.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8351.52
            },
            {
              ""Measurement_GUID"": ""262caa84-6d83-4243-b070-557e06fb7745"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 197164.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8382.0
            },
            {
              ""Measurement_GUID"": ""9a99cf17-de39-4701-8df3-780d79f1802b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 197831.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8412.48
            },
            {
              ""Measurement_GUID"": ""f725f6ef-6c82-4008-92a3-c49f6df461c4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 198265.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8442.96
            },
            {
              ""Measurement_GUID"": ""61c3e1a7-a47a-4d52-bb71-d8570aae611c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 198465.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8473.44
            },
            {
              ""Measurement_GUID"": ""ab401901-1f32-4530-b994-b285f802d5c7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 198799.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8503.92
            },
            {
              ""Measurement_GUID"": ""dfd19c94-0e69-45f3-8ad3-2e6e7d1d956d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 199199.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8534.4
            },
            {
              ""Measurement_GUID"": ""0404621c-7289-4cf6-a217-46b4f9bc80aa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 199666.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8564.88
            },
            {
              ""Measurement_GUID"": ""9059f439-61ee-4e50-ac9a-a68f03b0c4b6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 199933.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8595.36
            },
            {
              ""Measurement_GUID"": ""b25b8d6f-2918-4046-8cfe-0d9b4d510bd1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 200234.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8625.84
            },
            {
              ""Measurement_GUID"": ""d7c0862c-29d8-48ff-aa98-53471e59d4b9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 200667.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8656.32
            },
            {
              ""Measurement_GUID"": ""1e5a4756-d6a2-4fdd-b784-81024ac1c6b4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 201235.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8686.8
            },
            {
              ""Measurement_GUID"": ""a5344e2e-98f5-48d0-82da-e667e94cd745"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 201468.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8717.28
            },
            {
              ""Measurement_GUID"": ""c58a75f2-2346-470c-ab47-3611cdb9a4b3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 201702.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8747.76
            },
            {
              ""Measurement_GUID"": ""2d5fd8a3-e491-4ab8-bf91-9cfadcf5d229"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 202336.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8778.24
            },
            {
              ""Measurement_GUID"": ""d26d3d55-7b00-41a5-b23c-b4e04a00d829"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 202636.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8808.72
            },
            {
              ""Measurement_GUID"": ""e87d9851-a652-4527-90f7-8cf0802e4c9b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 202836.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8839.2
            },
            {
              ""Measurement_GUID"": ""37d4ecfd-5974-44e0-a02c-2e67d17ac4f0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 203437.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8869.68
            },
            {
              ""Measurement_GUID"": ""4fcf7463-1b49-407a-a379-53bd8694eeff"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 203804.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8900.16
            },
            {
              ""Measurement_GUID"": ""c0b8e56a-6350-4ebe-858c-d564b2bb1556"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 204004.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8930.64
            },
            {
              ""Measurement_GUID"": ""db6f964c-8596-4240-9412-2718d6b3fe48"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 204471.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8961.12
            },
            {
              ""Measurement_GUID"": ""010155f9-73c9-4c8d-990f-10a994b3d802"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 204972.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 8991.6
            },
            {
              ""Measurement_GUID"": ""7df1d119-22be-420f-84b4-3e829c5ec783"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 205138.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9022.08
            },
            {
              ""Measurement_GUID"": ""ce0751cd-4174-4fad-8b1b-0a3a6e068d70"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 205372.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9052.56
            },
            {
              ""Measurement_GUID"": ""2ceaf1f1-90bd-4a58-b46b-36cbed6168d0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 206006.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9083.04
            },
            {
              ""Measurement_GUID"": ""425b26a4-0020-40b5-b246-c0692f3fb5be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 206273.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9113.52
            },
            {
              ""Measurement_GUID"": ""f181b820-18d9-4d7a-85fb-9a8cef90553b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 206573.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9144.0
            },
            {
              ""Measurement_GUID"": ""537815a4-88c8-434f-88d2-52ed60386dfc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 207207.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9174.48
            },
            {
              ""Measurement_GUID"": ""48d65e3e-732e-4132-b23c-78bbbe804383"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 207541.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9204.96
            },
            {
              ""Measurement_GUID"": ""92b923e9-b1d1-4756-ab30-1de7afbc360f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 207741.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9235.44
            },
            {
              ""Measurement_GUID"": ""52639170-493c-4ddb-8a5e-2c8b25e307d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 208141.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9265.92
            },
            {
              ""Measurement_GUID"": ""cf041cb7-c794-46e3-98c0-5d432778e918"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 208475.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9296.4
            },
            {
              ""Measurement_GUID"": ""f26e3764-c27d-4bf6-b06b-c3f3fba75901"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 208942.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9326.88
            },
            {
              ""Measurement_GUID"": ""09ad6995-3035-4daf-995b-8ee8d0ccee3a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 209510.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9357.36
            },
            {
              ""Measurement_GUID"": ""7b7aad0a-c336-46e0-9462-b43c1df60c85"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 209676.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9387.84
            },
            {
              ""Measurement_GUID"": ""95089e0e-3199-4df7-8408-e5fad8262f99"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 209843.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9418.32
            },
            {
              ""Measurement_GUID"": ""75a921b2-11c3-4c8c-ba01-537707db7fab"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 210377.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9448.8
            },
            {
              ""Measurement_GUID"": ""519a3a35-66d0-42b9-a9f7-0be7679c8608"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 210811.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9479.28
            },
            {
              ""Measurement_GUID"": ""a2593024-4678-40ff-975f-c132e7496457"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 211011.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9509.76
            },
            {
              ""Measurement_GUID"": ""1e32ebe9-5195-4d27-8fc4-a2ca0442e5d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 211211.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9540.24
            },
            {
              ""Measurement_GUID"": ""907abb63-ba57-40f0-bbc7-5e44a54dddbd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 211612.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9570.72
            },
            {
              ""Measurement_GUID"": ""a0ee2e07-e6e9-4d5d-9de0-f76cb8548d94"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 212012.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9601.2
            },
            {
              ""Measurement_GUID"": ""96fd07dc-6f89-42cd-a2a5-74c814be7fa6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 212379.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9631.68
            },
            {
              ""Measurement_GUID"": ""738b7b4a-d193-46fc-bdc2-ba68c3c2f5ea"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 212613.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9662.16
            },
            {
              ""Measurement_GUID"": ""f86b4849-030c-413e-a8f7-14d27c29515d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 212846.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9692.64
            },
            {
              ""Measurement_GUID"": ""1a6bb566-15e9-416d-bbba-b436c97a79c4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 213447.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9723.12
            },
            {
              ""Measurement_GUID"": ""0f9e89b3-648e-4854-bf28-0b31ce6d6982"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 213914.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9753.6
            },
            {
              ""Measurement_GUID"": ""672e452b-cae3-4e32-8da4-aef2305a5bad"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 214181.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9784.08
            },
            {
              ""Measurement_GUID"": ""e987da8a-4f18-41a5-b06c-0b43a4051137"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 214548.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9814.56
            },
            {
              ""Measurement_GUID"": ""92e5d62a-1c5c-4914-9171-88a72ee38cc4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 214815.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9845.04
            },
            {
              ""Measurement_GUID"": ""6c627ede-cf88-4c51-a5d2-db188441ff9c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 215148.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9875.52
            },
            {
              ""Measurement_GUID"": ""8baf5fd3-49df-4286-88b2-caa0a915bf0e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 215816.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9906.0
            },
            {
              ""Measurement_GUID"": ""db4c0d28-6895-47c0-be57-98525bee283c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 216116.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9936.48
            },
            {
              ""Measurement_GUID"": ""a870c4f4-5a7b-4c06-89e3-226967a4f3a0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 216316.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9966.96
            },
            {
              ""Measurement_GUID"": ""6677a098-9c1c-40b3-be7b-8c3cf2810bed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 216650.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 9997.44
            },
            {
              ""Measurement_GUID"": ""fa26185f-db12-4a5e-b3c4-8bb7dc15a0bb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 217184.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10027.92
            },
            {
              ""Measurement_GUID"": ""de0f9903-0d80-46b4-9008-4c71df515d76"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 217551.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10058.4
            },
            {
              ""Measurement_GUID"": ""df7d56c0-a465-48fe-87ac-448ceb96ae19"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 217951.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10088.88
            },
            {
              ""Measurement_GUID"": ""e6bf7888-8f6e-468e-b931-b23cbd306b35"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 218185.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10119.36
            },
            {
              ""Measurement_GUID"": ""986272d4-42a8-444b-a591-ab1f07606b64"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 218418.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10149.84
            },
            {
              ""Measurement_GUID"": ""d4940107-dd28-4e0a-a368-71974f6b4b10"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 218886.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10180.32
            },
            {
              ""Measurement_GUID"": ""5aae5e14-b3dc-455a-92ed-8765e44dbd40"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 219453.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10210.8
            },
            {
              ""Measurement_GUID"": ""ed21b18b-7420-4f5e-ba8d-03fdff4d061f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 219720.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10241.28
            },
            {
              ""Measurement_GUID"": ""d0194530-9ba7-47c9-9c1b-cb9cbebaa18d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 219953.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10271.76
            },
            {
              ""Measurement_GUID"": ""fbaf6d17-1fdd-4543-9363-d4ec9f004383"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 220287.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10302.24
            },
            {
              ""Measurement_GUID"": ""a604e67c-5565-4f03-abe4-325cf69273eb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 220554.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10332.72
            },
            {
              ""Measurement_GUID"": ""e4d3fbe2-919e-49d9-a9a1-d8a8b9d85afe"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 220988.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10363.2
            },
            {
              ""Measurement_GUID"": ""10033a79-f26d-42c0-b94b-0036f6b4bf6d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 221655.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10393.68
            },
            {
              ""Measurement_GUID"": ""18dbce5c-4d1c-4066-8942-52fae64311ed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 221922.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10424.16
            },
            {
              ""Measurement_GUID"": ""5122e5a3-d3e0-4e6c-aaef-8cfe1d52068f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 222055.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10454.64
            },
            {
              ""Measurement_GUID"": ""c2dd3f4f-6fd7-40fb-87a9-0ec692627927"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 222356.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10485.12
            },
            {
              ""Measurement_GUID"": ""ecaf1c8a-f4fe-4c1c-b70b-51d648fc2f5b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 222856.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10515.6
            },
            {
              ""Measurement_GUID"": ""42122f70-eb7a-4783-b543-ecc8e084a0d6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 223190.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10546.08
            },
            {
              ""Measurement_GUID"": ""3c3ef903-92ff-4a7b-9b2e-93912a47aed2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 223524.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10576.56
            },
            {
              ""Measurement_GUID"": ""5fe2def7-df05-493b-8614-f3c5c0d17293"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 223957.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10607.04
            },
            {
              ""Measurement_GUID"": ""4609cc1e-f157-4e7e-904d-33c3c1a3531e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 224424.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10637.52
            },
            {
              ""Measurement_GUID"": ""6f91c65a-51e5-4eb5-9137-174a9bf0682f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 224658.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10668.0
            },
            {
              ""Measurement_GUID"": ""6047b9ac-6f83-4083-8b4c-55dafdc3ef07"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 224858.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10698.48
            },
            {
              ""Measurement_GUID"": ""deb037a9-a4b9-4a93-8470-120259eab9f6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 225158.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10728.96
            },
            {
              ""Measurement_GUID"": ""7d0fe2fb-10db-413d-ad6e-b3c5fe21e558"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 225726.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10759.44
            },
            {
              ""Measurement_GUID"": ""43c71f02-1123-4f2b-94d8-8a411ce1faf6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 225959.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10789.92
            },
            {
              ""Measurement_GUID"": ""4cb362b0-0fa2-40c5-8998-f612f4058293"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 226193.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10820.4
            },
            {
              ""Measurement_GUID"": ""c46241bf-2e4a-48b6-8176-c1917024b644"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 226793.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10850.88
            },
            {
              ""Measurement_GUID"": ""96d26552-1421-49a2-9c4f-dc81a6e30cf0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 227060.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10881.36
            },
            {
              ""Measurement_GUID"": ""86032525-99b0-470e-8c6e-194a320e3001"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 227561.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10911.84
            },
            {
              ""Measurement_GUID"": ""a2850499-25d1-4ee9-bc39-b482c3887ddf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 227928.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10942.32
            },
            {
              ""Measurement_GUID"": ""058e56ac-98d7-4303-a512-52719e26f817"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 228161.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 10972.8
            },
            {
              ""Measurement_GUID"": ""eb6ec888-7566-4756-b1a1-5d9726d73b6e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 228395.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11003.28
            },
            {
              ""Measurement_GUID"": ""8ad74dca-82ea-46c5-820b-361df249b5f7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 228896.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11033.76
            },
            {
              ""Measurement_GUID"": ""53d1c138-29c3-4602-b095-6339715f60e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 229129.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11064.24
            },
            {
              ""Measurement_GUID"": ""74035aad-7899-49e4-822d-fc8ab7f2edf9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 229296.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11094.72
            },
            {
              ""Measurement_GUID"": ""eb04b0a4-f022-46d6-809c-1c9b3b59f650"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 229997.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11125.2
            },
            {
              ""Measurement_GUID"": ""b59cdec1-391b-4494-9268-1261a0e7cf7a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 230297.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11155.68
            },
            {
              ""Measurement_GUID"": ""6fc33fe6-a855-43bc-9588-a73de6b4d0f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 230497.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11186.16
            },
            {
              ""Measurement_GUID"": ""9052bd79-137c-4298-8775-ac481cf69eec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 231064.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11216.64
            },
            {
              ""Measurement_GUID"": ""d35860a4-a393-42f7-a78d-95c199613fe1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 231365.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11247.12
            },
            {
              ""Measurement_GUID"": ""a14e9313-b798-4652-999a-65b0458593e3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 231565.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11277.6
            },
            {
              ""Measurement_GUID"": ""58785ad5-31c2-44a7-a723-2ef80af22977"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 231765.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11308.08
            },
            {
              ""Measurement_GUID"": ""0f536332-4456-4e68-b05b-f795e1401977"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 232266.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11338.56
            },
            {
              ""Measurement_GUID"": ""151b9348-bbf8-4ca1-b356-5d4c4c394048"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 232699.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11369.04
            },
            {
              ""Measurement_GUID"": ""6af352f3-2d9e-4c7f-ab32-faedaeb75f41"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 232933.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11399.52
            },
            {
              ""Measurement_GUID"": ""73d0a22e-8596-4d5b-aea6-f44c2d625056"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 233267.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11430.0
            },
            {
              ""Measurement_GUID"": ""abb97615-27b8-4994-9abb-cb462559233f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 233600.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11460.48
            },
            {
              ""Measurement_GUID"": ""56b9dc71-38dc-4319-826b-8412f8b7d893"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 234168.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11490.96
            },
            {
              ""Measurement_GUID"": ""4dded8af-1ed6-41ae-bfd6-007a77e52778"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 234468.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11521.44
            },
            {
              ""Measurement_GUID"": ""0dc5f14f-1798-480e-92be-307f0759c752"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 234668.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11551.92
            },
            {
              ""Measurement_GUID"": ""ecf7821c-4f89-4289-8a0a-59d3b9f240ac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 235202.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11582.4
            },
            {
              ""Measurement_GUID"": ""d2935abc-67cf-4d83-b61e-ec16276a0b51"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 235836.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11612.88
            },
            {
              ""Measurement_GUID"": ""0b8d1285-20a3-4a6c-abd8-a7e0f649f25e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 236036.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11643.36
            },
            {
              ""Measurement_GUID"": ""7bd38be6-0e7f-496b-9139-c7173593836c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 236203.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11673.84
            },
            {
              ""Measurement_GUID"": ""e251bef0-1a89-4cad-95e9-bfc940b06c88"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 236503.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11704.32
            },
            {
              ""Measurement_GUID"": ""3e293eba-55f9-470e-8bda-c5cda91614f7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 236837.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11734.8
            },
            {
              ""Measurement_GUID"": ""5dc8d254-1715-4c71-bf69-fc5dfa4fc9ae"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 237237.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11765.28
            },
            {
              ""Measurement_GUID"": ""d27839d4-4ad5-4846-a7bc-8df1672ad94b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 237838.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11795.76
            },
            {
              ""Measurement_GUID"": ""6cc705e5-b392-4c14-a739-2bb57f7cfba7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 238305.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11826.24
            },
            {
              ""Measurement_GUID"": ""3d62d5cd-4841-4220-9a62-b4fdf6491f0c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 238472.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11856.72
            },
            {
              ""Measurement_GUID"": ""6cc19660-d1ab-43b4-a5a4-df56832e0b33"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 238672.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11887.2
            },
            {
              ""Measurement_GUID"": ""72ce4a54-5120-4a1b-a7c6-66896fdeef75"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 238906.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11917.68
            },
            {
              ""Measurement_GUID"": ""252c7c84-2133-4ccb-8ac9-9c74ed4c3978"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 239273.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11948.16
            },
            {
              ""Measurement_GUID"": ""b7c9b807-da6d-4c48-81ae-9aca52b478f8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 239873.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 11978.64
            },
            {
              ""Measurement_GUID"": ""8ad1a6a7-0a3b-4230-a47e-b5fb0947bce5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 240440.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12009.12
            },
            {
              ""Measurement_GUID"": ""12e79716-d7bd-48ce-9c88-f4035cc558d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 240641.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12039.6
            },
            {
              ""Measurement_GUID"": ""9b3a9fa4-7ae6-4044-85d2-6e6e4a1f0059"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 240841.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12070.08
            },
            {
              ""Measurement_GUID"": ""28cb2841-db59-42fb-8846-b9725e3c1165"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 241108.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12100.56
            },
            {
              ""Measurement_GUID"": ""54e84b96-2eb2-4147-b23a-1d5374344a94"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 241408.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12131.04
            },
            {
              ""Measurement_GUID"": ""277d59f3-9903-4f8d-a51a-7292ca64a0c4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 242075.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12161.52
            },
            {
              ""Measurement_GUID"": ""6ff128d8-8a99-4937-a3a3-0f701fe50d1d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 242509.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12192.0
            },
            {
              ""Measurement_GUID"": ""67234490-bd96-4a95-b753-41d102f8f4ff"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 242709.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12222.48
            },
            {
              ""Measurement_GUID"": ""7d2e69f8-ba11-407f-9dc8-02b83221effa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 242976.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12252.96
            },
            {
              ""Measurement_GUID"": ""e5b0c11f-b3b2-492e-9d6b-e2f5caa7d158"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 243377.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12283.44
            },
            {
              ""Measurement_GUID"": ""02418876-e11d-4ed8-b597-13bf0dafd519"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 243977.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12313.92
            },
            {
              ""Measurement_GUID"": ""bd7e6844-508f-47e9-927c-0743d602dcfd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 244144.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12344.4
            },
            {
              ""Measurement_GUID"": ""d2b3ba21-0dd3-4c12-9e9c-0a8cb4a94e0f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 244378.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12374.88
            },
            {
              ""Measurement_GUID"": ""26c69fa0-114e-4af8-8891-9b9cfce666fb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 244678.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12405.36
            },
            {
              ""Measurement_GUID"": ""c6b203ce-2c09-4305-b949-24805bacebe9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 245345.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12435.84
            },
            {
              ""Measurement_GUID"": ""a307f7c8-7a23-4efb-8eef-b3c0064bd16f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 245579.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12466.32
            },
            {
              ""Measurement_GUID"": ""578b9a9e-6ddb-4fc5-8a2c-4ebc110130b7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 245812.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12496.8
            },
            {
              ""Measurement_GUID"": ""3928203e-9cae-4f9f-aa49-45aafac7369a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 246113.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12527.28
            },
            {
              ""Measurement_GUID"": ""81dab6dd-64c5-41de-8596-9935a4a783ef"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 246747.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12557.76
            },
            {
              ""Measurement_GUID"": ""38e43b2d-1815-495d-a990-1de3bd027060"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 246880.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12588.24
            },
            {
              ""Measurement_GUID"": ""3dcf7056-7a9d-40d3-bfcf-e79ff71190f7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 247080.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12618.72
            },
            {
              ""Measurement_GUID"": ""87489b2c-4a44-4ba2-a5c8-7d2cdf9faa2f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 247748.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12649.2
            },
            {
              ""Measurement_GUID"": ""861d1c8e-7a89-465b-b55f-d07ec1508fb0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 247981.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12679.68
            },
            {
              ""Measurement_GUID"": ""83a0a435-13c2-45d7-adc7-eca690e64e5c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 248182.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12710.16
            },
            {
              ""Measurement_GUID"": ""f4426b90-d710-466c-866f-56d580a29fe2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 248549.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12740.64
            },
            {
              ""Measurement_GUID"": ""3bcbb0cd-0cde-4197-ba8c-2f671d5008d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 248949.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12771.12
            },
            {
              ""Measurement_GUID"": ""d120e855-3885-45dc-a78c-54635371a408"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 249283.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12801.6
            },
            {
              ""Measurement_GUID"": ""9ba24cac-6f50-4e1b-a21b-c26c7daa1821"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 249583.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12832.08
            },
            {
              ""Measurement_GUID"": ""e2505a4e-0935-427e-b747-e0411f73e252"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 250050.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12862.56
            },
            {
              ""Measurement_GUID"": ""c1d40b0f-0514-4e8a-8805-94fd546ef30c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 250584.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12893.04
            },
            {
              ""Measurement_GUID"": ""3fd81ee1-2cd0-4d0d-a499-410a5d2f9a9c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 250751.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12923.52
            },
            {
              ""Measurement_GUID"": ""2e650149-35a1-442a-9e90-24d327a8629a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 251018.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12954.0
            },
            {
              ""Measurement_GUID"": ""ea90ac34-883c-4408-a717-80c2d6c8312b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 251385.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 12984.48
            },
            {
              ""Measurement_GUID"": ""baafceee-d41b-43ee-ab03-5ab48c5c771e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 251852.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13014.96
            },
            {
              ""Measurement_GUID"": ""6df26f83-f426-4e2f-a69e-d4a6e507ec66"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 252052.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13045.44
            },
            {
              ""Measurement_GUID"": ""f126fe97-3efd-4135-93eb-c7853de1a312"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 252452.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13075.92
            },
            {
              ""Measurement_GUID"": ""4935cb67-15de-415f-b676-178675b23084"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 253020.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13106.4
            },
            {
              ""Measurement_GUID"": ""970ca65a-ff8d-4260-b430-522e89c41741"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 253220.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13136.88
            },
            {
              ""Measurement_GUID"": ""fc68607f-3d4d-44c5-a636-7c47cbb9aec0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 253453.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13167.36
            },
            {
              ""Measurement_GUID"": ""636f2a91-b6eb-464c-a4fc-7ead5701446a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 254121.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13197.84
            },
            {
              ""Measurement_GUID"": ""08f8e993-5200-4aaa-99d6-2fce6b4552be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 254388.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13228.32
            },
            {
              ""Measurement_GUID"": ""e78e706e-9bba-403a-b279-1a0341951581"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 254588.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13258.8
            },
            {
              ""Measurement_GUID"": ""0e2bc709-4cd5-4257-a297-4de81f3d4f9d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 255022.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13289.28
            },
            {
              ""Measurement_GUID"": ""57fe25a7-c966-4084-a667-75ecb9ffa56b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 255589.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13319.76
            },
            {
              ""Measurement_GUID"": ""0b60efdf-f6c3-4e0f-927b-24f6f237b325"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 255822.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13350.24
            },
            {
              ""Measurement_GUID"": ""38b6ed25-0bad-4821-9f31-ae40920e54fd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 255989.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13380.72
            },
            {
              ""Measurement_GUID"": ""c4549774-a56e-404c-b24d-7fcefadb04fe"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 256356.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13411.2
            },
            {
              ""Measurement_GUID"": ""3718a1ea-e597-4987-9b73-c8e120fe9d7b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 256790.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13441.68
            },
            {
              ""Measurement_GUID"": ""bec10bad-74e9-4b33-886a-85a4869ac622"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 257191.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13472.16
            },
            {
              ""Measurement_GUID"": ""eba3d819-df56-4835-b88c-2e3611f1bfa0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 257524.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13502.64
            },
            {
              ""Measurement_GUID"": ""c7ebc9db-0656-495c-b432-a9f06d167781"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 257724.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13533.12
            },
            {
              ""Measurement_GUID"": ""e8d53f41-6194-458e-bd80-c8b9582ed38c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 258058.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13563.6
            },
            {
              ""Measurement_GUID"": ""6f2cac53-a09a-4b2f-8cb1-648cb23d748d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 258792.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13594.08
            },
            {
              ""Measurement_GUID"": ""d7a5e74e-5a16-48af-8ac8-e1b09e223853"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 258892.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13624.56
            },
            {
              ""Measurement_GUID"": ""d286f67d-88e4-4451-a850-43a531b520ec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 259092.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13655.04
            },
            {
              ""Measurement_GUID"": ""0acee863-2ef5-4717-a68c-da20d1e4d1eb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 259493.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13685.52
            },
            {
              ""Measurement_GUID"": ""97f7bde5-55ca-49cc-86ae-109998a26281"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 259893.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13716.0
            },
            {
              ""Measurement_GUID"": ""84c1c611-1e97-4a95-8862-0f412781f18c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 260527.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13746.48
            },
            {
              ""Measurement_GUID"": ""b76fad6d-37a4-459a-bb4d-13b14e74cd35"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 260761.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13776.96
            },
            {
              ""Measurement_GUID"": ""c16fdf6d-c718-4106-8969-e37d8ce6bd43"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 260961.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13807.44
            },
            {
              ""Measurement_GUID"": ""ba156be3-6666-4bd8-8acc-a57ad3967a46"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 261195.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13837.92
            },
            {
              ""Measurement_GUID"": ""95b4ca07-c144-4f88-82db-0952ccb0c7d3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 261695.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13868.4
            },
            {
              ""Measurement_GUID"": ""dc5c4bd1-afe0-4f70-9cb5-51094d8d3811"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 262095.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13898.88
            },
            {
              ""Measurement_GUID"": ""bd00a070-2059-4131-a679-83f5e324bcf4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 262496.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13929.36
            },
            {
              ""Measurement_GUID"": ""3f5bbf63-ab79-4347-b09c-e835e991e250"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 262796.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13959.84
            },
            {
              ""Measurement_GUID"": ""cf740fe8-ac85-4b8d-a9c2-23c0670d0c42"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 263030.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 13990.32
            },
            {
              ""Measurement_GUID"": ""44eb8f60-ec77-4292-84ec-44f20e783673"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 263297.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14020.8
            },
            {
              ""Measurement_GUID"": ""d7adbaa6-67c7-4870-a18c-cf85317510b0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 263864.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14051.28
            },
            {
              ""Measurement_GUID"": ""affba00e-fff4-4e11-ad41-7ed8eef1a515"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 264331.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14081.76
            },
            {
              ""Measurement_GUID"": ""8a5a5b0a-7f58-45fa-a999-8ba0d5b57d0f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 264565.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14112.24
            },
            {
              ""Measurement_GUID"": ""cd0c8aeb-0a52-4cd2-8300-a75bdb54815e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 264765.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14142.72
            },
            {
              ""Measurement_GUID"": ""592e5ba7-3c21-4f0b-9561-271a8ec0425d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 265098.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14173.2
            },
            {
              ""Measurement_GUID"": ""d619a1d0-32a9-4e08-a773-0ddd43abd460"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 265432.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14203.68
            },
            {
              ""Measurement_GUID"": ""b91e3377-6d24-4245-81d3-c8f1e5192e9f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 266033.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14234.16
            },
            {
              ""Measurement_GUID"": ""5380cc95-ff8b-4dbc-80a5-d3dbe4592a17"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 266266.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14264.64
            },
            {
              ""Measurement_GUID"": ""7c45661b-7a2e-4133-b64e-e8060d5728df"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 266466.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14295.12
            },
            {
              ""Measurement_GUID"": ""0cf010bd-439b-4094-ab2d-d582bcb2c2e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 266733.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14325.6
            },
            {
              ""Measurement_GUID"": ""673063af-8280-46ae-807f-a7e27125b868"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 267401.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14356.08
            },
            {
              ""Measurement_GUID"": ""dfbdfd4a-e6cf-422e-a9f5-b04fd68b9d65"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 267701.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14386.56
            },
            {
              ""Measurement_GUID"": ""ecd37f65-70ac-424b-a3bc-44d427e3300e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 267935.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14417.04
            },
            {
              ""Measurement_GUID"": ""64bea200-2a62-41eb-9fdf-d514d5c889f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 268135.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14447.52
            },
            {
              ""Measurement_GUID"": ""3d882228-7ded-4caa-8f49-cf2c9c4ca2f5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 268602.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14478.0
            },
            {
              ""Measurement_GUID"": ""28152ec5-9cad-419c-bfab-437ed5353e44"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 269136.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14508.48
            },
            {
              ""Measurement_GUID"": ""7b977934-85bf-4c94-b739-8fb57b6ab7f5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 269369.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14538.96
            },
            {
              ""Measurement_GUID"": ""be60a444-cc4f-4fba-8c76-845fa93a7b25"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 269636.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14569.44
            },
            {
              ""Measurement_GUID"": ""e818aaa1-7cfe-43ae-8477-0c85174dcdc0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 270237.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14599.92
            },
            {
              ""Measurement_GUID"": ""31874571-20f5-4ccd-88cc-dab5f797aa8c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 270470.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14630.4
            },
            {
              ""Measurement_GUID"": ""84e401a4-5aea-4473-831d-53568952abe8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 270671.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14660.88
            },
            {
              ""Measurement_GUID"": ""1184ba14-65bc-40a3-9c2a-9d23b56589f6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 271071.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14691.36
            },
            {
              ""Measurement_GUID"": ""c988cfbf-ccf7-4097-8823-39b7796414aa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 271471.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14721.84
            },
            {
              ""Measurement_GUID"": ""2deb32e0-4cf5-4fd4-8a40-a23d2f7c4061"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 271705.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14752.32
            },
            {
              ""Measurement_GUID"": ""5715fd21-dff8-4f02-9aec-9531dca4106b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 272039.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14782.8
            },
            {
              ""Measurement_GUID"": ""0269920c-6b6d-475e-8a31-f65e36ece004"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 272606.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14813.28
            },
            {
              ""Measurement_GUID"": ""45d41240-3185-4391-9054-b949668afcd9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 272806.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14843.76
            },
            {
              ""Measurement_GUID"": ""8e36635c-1f72-4df6-90c0-8fac637c1c96"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 273073.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14874.24
            },
            {
              ""Measurement_GUID"": ""9a498efd-ec14-4a52-8d77-26c272476e47"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 273740.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14904.72
            },
            {
              ""Measurement_GUID"": ""1e61c85a-caf8-431b-9a96-366228a29b61"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 273941.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14935.2
            },
            {
              ""Measurement_GUID"": ""f1dfde86-fb24-4230-a082-e0c1415bd441"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 274107.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14965.68
            },
            {
              ""Measurement_GUID"": ""10b76251-51e4-4c47-bdc0-f88e7929c954"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 274641.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 14996.16
            },
            {
              ""Measurement_GUID"": ""c6941d62-e4b6-4306-9676-a16c5ac0f0ac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 275008.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15026.64
            },
            {
              ""Measurement_GUID"": ""a06ba0da-4a03-4d71-915f-6ae8c8462f95"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 275175.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15057.12
            },
            {
              ""Measurement_GUID"": ""461ca0d9-d159-4aaf-a973-b1a8c03afd64"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 275475.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15087.6
            },
            {
              ""Measurement_GUID"": ""af308cc8-c878-4493-8c75-959ecc222501"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 276009.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15118.08
            },
            {
              ""Measurement_GUID"": ""dfa88fb7-c4be-4513-a84e-f59c82c76869"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 276376.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15148.56
            },
            {
              ""Measurement_GUID"": ""b51f80e4-206c-434f-a084-1f83daad76f5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 276610.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15179.04
            },
            {
              ""Measurement_GUID"": ""3e9e53ab-1d2d-4a6a-8e9b-630cc0e311e4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 276977.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15209.52
            },
            {
              ""Measurement_GUID"": ""e059110a-4172-4bbd-9cca-2c5fc6ab6bec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 277578.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15240.0
            },
            {
              ""Measurement_GUID"": ""457259d6-b7fd-4e92-ba85-7c7bbd3065d6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 277811.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15270.48
            },
            {
              ""Measurement_GUID"": ""2d9ade28-77f7-4b8d-8ca1-0413fc1250db"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 278011.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15300.96
            },
            {
              ""Measurement_GUID"": ""162a2ea4-2027-4145-840c-c17016056696"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 278278.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15331.44
            },
            {
              ""Measurement_GUID"": ""a7e15571-e8d8-4f92-8a3c-df033fff3b08"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 279012.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15361.92
            },
            {
              ""Measurement_GUID"": ""ae05448f-4647-4ea5-817d-28cae4d962fb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 279213.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15392.4
            },
            {
              ""Measurement_GUID"": ""7c4944ef-587e-4ca4-a28f-ac120ebecbd1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 279413.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15422.88
            },
            {
              ""Measurement_GUID"": ""442c3b95-3d68-4cac-9776-8d274c3b4e5e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 279813.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15453.36
            },
            {
              ""Measurement_GUID"": ""7c4f69a8-2a3d-48a9-a36d-88b332743ddb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 280314.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15483.84
            },
            {
              ""Measurement_GUID"": ""58e76070-22fe-402a-aa2c-49a6dd4d0b0d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 280547.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15514.32
            },
            {
              ""Measurement_GUID"": ""af295965-272b-4e98-be8f-d3f6b5bff002"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 280747.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15544.8
            },
            {
              ""Measurement_GUID"": ""368caf7b-5762-4743-ac30-e6e5af2361db"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 281081.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15575.28
            },
            {
              ""Measurement_GUID"": ""290c7f95-dd88-460e-9569-d5803e088257"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 281548.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15605.76
            },
            {
              ""Measurement_GUID"": ""5b1993de-3f71-4390-a082-12cf26ead047"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 282316.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15636.24
            },
            {
              ""Measurement_GUID"": ""2b7f826f-5a90-4eae-b026-44df01e821cb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 282649.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15666.72
            },
            {
              ""Measurement_GUID"": ""f1b28ac6-dcfa-4214-a027-08719ee6af83"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 282783.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15697.2
            },
            {
              ""Measurement_GUID"": ""0b0d3262-a2a6-4c51-9c35-d1ea20cb33ce"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 282950.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15727.68
            },
            {
              ""Measurement_GUID"": ""1be2aa08-7d5f-4e69-8554-e4b129cb8d73"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 283851.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15758.16
            },
            {
              ""Measurement_GUID"": ""c8991b42-9895-40fe-8a93-94ee39658bfb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 286253.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15788.64
            },
            {
              ""Measurement_GUID"": ""44a41046-2619-4615-bc9a-51b7c186ac7d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 288355.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15819.12
            },
            {
              ""Measurement_GUID"": ""de18ddfc-708a-4447-b726-20b6898e0c9e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 288889.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15849.6
            },
            {
              ""Measurement_GUID"": ""1e13fd6b-2f84-4142-a731-959d4bdf8fd4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 293927.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15880.08
            },
            {
              ""Measurement_GUID"": ""6bcdae71-bf4d-4574-aa11-61c1888ef3a5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 345379.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15910.56
            },
            {
              ""Measurement_GUID"": ""56c8ffb6-a6b3-4e53-b242-f1cdf6f1b6be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 346947.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15941.04
            },
            {
              ""Measurement_GUID"": ""1feb353b-358e-444e-83ee-0b278f30795d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 347147.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 15971.52
            },
            {
              ""Measurement_GUID"": ""1cf30454-2dbc-4711-a406-b80857af69ab"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 347347.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16002.0
            },
            {
              ""Measurement_GUID"": ""36e3efeb-21d0-4200-8b22-970d85253229"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 347681.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16032.48
            },
            {
              ""Measurement_GUID"": ""e2468ba7-1528-4528-a9c5-acceea459b53"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 348215.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16062.96
            },
            {
              ""Measurement_GUID"": ""71f41c4e-704c-43ab-8b7d-5c7b484d8f51"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 348849.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16093.44
            },
            {
              ""Measurement_GUID"": ""3881f564-8de7-42e4-82cc-d6be0b8254a8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 349216.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16123.92
            },
            {
              ""Measurement_GUID"": ""297971c0-01f3-4bad-8012-c09c5c71196c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 349416.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16154.4
            },
            {
              ""Measurement_GUID"": ""1a174ed3-4515-49f2-92f8-393e197c23a1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 349616.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16184.88
            },
            {
              ""Measurement_GUID"": ""6692f295-e761-4623-bcef-32fe30342264"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 350250.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16215.36
            },
            {
              ""Measurement_GUID"": ""e9973729-dac2-4706-b8ab-a55867f2607a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 350784.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16245.84
            },
            {
              ""Measurement_GUID"": ""415e35b3-5e50-462d-befe-db40b9b33382"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 351151.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16276.32
            },
            {
              ""Measurement_GUID"": ""6f78d5e5-67f8-4f6b-a1f7-f8b8ca878a63"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 351385.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16306.8
            },
            {
              ""Measurement_GUID"": ""bfc4d54c-ec11-4e18-8dd9-a2fd3e9afd54"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 351718.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16337.28
            },
            {
              ""Measurement_GUID"": ""313dd8ef-1c6f-44d4-bd21-4d344d95b875"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 352085.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16367.76
            },
            {
              ""Measurement_GUID"": ""b301e320-dff1-47b9-aba2-5a4f01fc6bc8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 352953.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16398.24
            },
            {
              ""Measurement_GUID"": ""b9585d45-44a8-4c07-8c63-904848e1d184"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 353353.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16428.72
            },
            {
              ""Measurement_GUID"": ""f5b8156b-618b-467f-8805-c77151c963d3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 353520.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16459.2
            },
            {
              ""Measurement_GUID"": ""1b530278-90a6-46d9-8ea8-373d5e0357b8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 353720.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16489.68
            },
            {
              ""Measurement_GUID"": ""c60d1c51-91e9-41dd-841c-e0679d1767de"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 354121.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16520.16
            },
            {
              ""Measurement_GUID"": ""445abf00-8356-4284-904f-b81963538b27"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 354855.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16550.64
            },
            {
              ""Measurement_GUID"": ""2cc99d15-ec15-497b-8395-109f45172884"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 355022.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16581.12
            },
            {
              ""Measurement_GUID"": ""c7ea4953-930b-41c5-a404-de597d41ba79"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 355289.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16611.6
            },
            {
              ""Measurement_GUID"": ""98aae0e3-739c-4f19-ada6-1b5625eb5cdc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 355856.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16642.08
            },
            {
              ""Measurement_GUID"": ""de417951-63df-4b06-b988-2cd0b5b9f259"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 356223.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16672.56
            },
            {
              ""Measurement_GUID"": ""60e341e4-4fc4-4eef-b117-587734535c11"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 356557.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16703.04
            },
            {
              ""Measurement_GUID"": ""ebe2546d-62cb-4a7e-aff9-0be4dce3f508"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 356890.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16733.52
            },
            {
              ""Measurement_GUID"": ""017e8382-8cd5-4b6a-9759-af80bebbbfa3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 357658.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16764.0
            },
            {
              ""Measurement_GUID"": ""32c8b020-9c01-4874-a8b8-4d77833b43be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 357991.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16794.48
            },
            {
              ""Measurement_GUID"": ""4ea54942-7b4b-4b50-adc7-24fb7b8aa16f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 358292.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16824.96
            },
            {
              ""Measurement_GUID"": ""6b80c8ab-534d-4d6d-b152-ece0828cf0c8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 359092.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16855.44
            },
            {
              ""Measurement_GUID"": ""71c5edf9-24a7-4dbe-a7ba-5a86c58cda74"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 359293.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16885.92
            },
            {
              ""Measurement_GUID"": ""5e057dfd-3762-46d7-9eb7-58d4c4323c44"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 359526.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16916.4
            },
            {
              ""Measurement_GUID"": ""8f9ac174-60eb-47c2-86bb-c0fa65eea44e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 360127.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16946.88
            },
            {
              ""Measurement_GUID"": ""ae70325b-df27-4335-b74f-933274d776d9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 360460.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 16977.36
            },
            {
              ""Measurement_GUID"": ""d16e5b1f-be2b-43b8-81bb-3bcf5d49d4cb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 360661.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17007.84
            },
            {
              ""Measurement_GUID"": ""084eb8d0-5ac5-4094-acd6-4d191c00c9d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 361461.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17038.32
            },
            {
              ""Measurement_GUID"": ""d51e0179-f5fe-4ad9-aab4-dee1baabeef6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 361728.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17068.8
            },
            {
              ""Measurement_GUID"": ""baebadf1-214c-41d2-80fe-daf3e79f2db5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 361929.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17099.28
            },
            {
              ""Measurement_GUID"": ""bde15767-4bfd-48c6-9833-7436061227b8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 362129.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17129.76
            },
            {
              ""Measurement_GUID"": ""10da3114-026b-44dc-86ca-41e5c9de5002"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 362930.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17160.24
            },
            {
              ""Measurement_GUID"": ""2f902f02-6ab1-470f-84df-7aed9f1e9f54"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 363163.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17190.72
            },
            {
              ""Measurement_GUID"": ""00549135-d07c-4014-8ade-92be07e72dbf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 363363.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17221.2
            },
            {
              ""Measurement_GUID"": ""fa2e5b48-6621-403a-b5c9-52c5d823c54c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 364031.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17251.68
            },
            {
              ""Measurement_GUID"": ""1367d320-2468-4ac5-9350-9420b4cbf223"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 364331.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17282.16
            },
            {
              ""Measurement_GUID"": ""70c24efa-38a6-4cde-8df8-e12ecca6ca9f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 364531.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17312.64
            },
            {
              ""Measurement_GUID"": ""83781b34-0fda-480b-ae21-1309a674af4c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 364965.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17343.12
            },
            {
              ""Measurement_GUID"": ""d640a465-e90f-408b-8348-84c3e6d0fbc4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 365666.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17373.6
            },
            {
              ""Measurement_GUID"": ""b011290a-ac56-4526-b160-05bd95fd8aa2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 365866.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17404.08
            },
            {
              ""Measurement_GUID"": ""693e7a9c-5298-4eb2-897c-1656a6be08f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 366133.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17434.56
            },
            {
              ""Measurement_GUID"": ""bcd673e7-44a4-4ed0-a881-5374c9cd19b5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 366667.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17465.04
            },
            {
              ""Measurement_GUID"": ""eab0253e-886d-4881-8f1f-60f620b8e444"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 367034.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17495.52
            },
            {
              ""Measurement_GUID"": ""741b3b05-aa72-4520-a1e0-cdd5a62e86ac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 367201.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17526.0
            },
            {
              ""Measurement_GUID"": ""5059b57a-ca5e-45e3-a917-a6214d851378"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 367434.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17556.48
            },
            {
              ""Measurement_GUID"": ""7acf6007-5242-4467-a11b-6576071375f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 367634.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17586.96
            },
            {
              ""Measurement_GUID"": ""6f93909a-fe33-4e43-845d-2f8c38d16c86"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 368569.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17617.44
            },
            {
              ""Measurement_GUID"": ""4244700d-4522-4aff-ab5c-dd72107071db"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 368902.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17647.92
            },
            {
              ""Measurement_GUID"": ""8d1d6af6-505e-4bcb-84b2-09891bb9d903"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 369136.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17678.4
            },
            {
              ""Measurement_GUID"": ""722c9ce3-4975-496b-9b7d-7b8cdb4519fd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 369369.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17708.88
            },
            {
              ""Measurement_GUID"": ""2c1d9c25-cf6f-4be8-9c81-744a7b707eb9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 369937.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17739.36
            },
            {
              ""Measurement_GUID"": ""62301c63-8c9f-4cc6-976c-3f5e08fd9751"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 370571.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17769.84
            },
            {
              ""Measurement_GUID"": ""c0d0d07e-5414-450a-acc5-5053d4c73147"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 370771.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17800.32
            },
            {
              ""Measurement_GUID"": ""f599f0c8-bfbc-4412-b47c-99be6c24e637"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 371038.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17830.8
            },
            {
              ""Measurement_GUID"": ""f6b9c7db-4268-4bac-8c89-1bf6ba3545b5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 371338.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17861.28
            },
            {
              ""Measurement_GUID"": ""f03ed63f-e330-4799-8a7e-e755daffab8d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 371872.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17891.76
            },
            {
              ""Measurement_GUID"": ""90b33c2f-1b06-4f13-a46b-aaaf465d29c8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 372472.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17922.24
            },
            {
              ""Measurement_GUID"": ""28ada96a-270a-46b7-88c5-6031c949c579"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 372739.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17952.72
            },
            {
              ""Measurement_GUID"": ""239b4797-3273-4e6b-ac50-3cd7ef7af3d8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 372940.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 17983.2
            },
            {
              ""Measurement_GUID"": ""7f1ab03c-abf2-4210-8ec8-725ba3d26a69"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 373240.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18013.68
            },
            {
              ""Measurement_GUID"": ""8d085e6e-9ac9-4fdc-87d3-e51ed7b8706e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 373540.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18044.16
            },
            {
              ""Measurement_GUID"": ""d61ada83-15a5-4b4b-8339-4cabfc927d6e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 374241.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18074.64
            },
            {
              ""Measurement_GUID"": ""ce0f6614-23d3-4a47-8f81-d43438d97bd9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 374741.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18105.12
            },
            {
              ""Measurement_GUID"": ""e3e93908-18e9-4af6-ab63-b4ca12ef3a23"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 374942.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18135.6
            },
            {
              ""Measurement_GUID"": ""326259a8-c0ab-449f-bdfe-1d1ce049144c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 375108.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18166.08
            },
            {
              ""Measurement_GUID"": ""ec6eb268-8baa-4697-ac60-a4f7d4daed71"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 375642.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18196.56
            },
            {
              ""Measurement_GUID"": ""a5c159cc-c4b3-4f64-a2bf-4d4c081b364c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 376109.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18227.04
            },
            {
              ""Measurement_GUID"": ""bbbd98ec-9e81-4e9a-ba89-bef119a99b4f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 376677.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18257.52
            },
            {
              ""Measurement_GUID"": ""cda91c4d-00fe-4dab-a9b2-146478bb8830"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 376910.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18288.0
            },
            {
              ""Measurement_GUID"": ""57b68b85-ee07-4f55-bb58-a82754448c8c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 377077.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18318.48
            },
            {
              ""Measurement_GUID"": ""229955a8-2097-4dba-80a1-70607c30fae9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 377411.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18348.96
            },
            {
              ""Measurement_GUID"": ""64d524f8-dd6c-493c-a342-0215e81d7eae"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 378145.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18379.44
            },
            {
              ""Measurement_GUID"": ""3d81abd6-3859-487e-8de7-b048f14a3793"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 378679.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18409.92
            },
            {
              ""Measurement_GUID"": ""4e63c3b2-e2c1-49a6-8505-9c000ea009cc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 378879.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18440.4
            },
            {
              ""Measurement_GUID"": ""31368c78-18ea-4f09-b937-145318db6d31"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 379046.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18470.88
            },
            {
              ""Measurement_GUID"": ""920bb188-8d2d-459f-b8dd-b86a4c272a8f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 379346.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18501.36
            },
            {
              ""Measurement_GUID"": ""38da57da-3bde-4222-a9ab-dee55e834e6c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 380047.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18531.84
            },
            {
              ""Measurement_GUID"": ""4ef2cd65-b502-4171-b640-52386b8d81e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 380314.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18562.32
            },
            {
              ""Measurement_GUID"": ""279eaa5e-1bc0-4d36-aed3-cf9565218eaa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 380581.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18592.8
            },
            {
              ""Measurement_GUID"": ""a1158a8f-5efb-4010-be39-b5b96512575a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 381014.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18623.28
            },
            {
              ""Measurement_GUID"": ""cffae19e-2972-4afa-bfc0-7b1097fafb35"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 381481.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18653.76
            },
            {
              ""Measurement_GUID"": ""ba8d3c50-7517-4df8-910c-8ad74d6e7366"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 381748.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18684.24
            },
            {
              ""Measurement_GUID"": ""b9194425-a42f-4791-b5c1-cff38a9c43f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 381915.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18714.72
            },
            {
              ""Measurement_GUID"": ""d0baa8d9-8e63-497a-9e6d-743c4a7e1db1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 382583.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18745.2
            },
            {
              ""Measurement_GUID"": ""68be6497-d934-4f89-ab09-2978a7c866a1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 382916.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18775.68
            },
            {
              ""Measurement_GUID"": ""69d92496-f70e-4134-a597-2b0bbd07060b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 383116.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18806.16
            },
            {
              ""Measurement_GUID"": ""d2c93f05-da8e-47f2-a70e-c01e784ef8b7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 383383.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18836.64
            },
            {
              ""Measurement_GUID"": ""075c25a4-2a6d-439d-a576-d76f4e2178fd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 384284.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18867.12
            },
            {
              ""Measurement_GUID"": ""8980ee8d-2298-4020-9320-b06d301eafa0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 384518.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18897.6
            },
            {
              ""Measurement_GUID"": ""866dfdb2-83c6-4639-95f1-e2a82aeb82cc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 384685.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18928.08
            },
            {
              ""Measurement_GUID"": ""41f45f05-9cc8-4e98-9405-8fb5f8b463ec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 385052.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18958.56
            },
            {
              ""Measurement_GUID"": ""023f0a0d-1e6f-4f04-80ae-8a472c81f609"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 385652.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 18989.04
            },
            {
              ""Measurement_GUID"": ""0103c90c-7545-4aa9-a23a-8ce83bbad019"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 385853.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19019.52
            },
            {
              ""Measurement_GUID"": ""e1941351-88a1-4f3a-9345-e42603bb90d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 386186.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19050.0
            },
            {
              ""Measurement_GUID"": ""eea45866-e4cb-4494-bc87-6acd2d3cd56d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 386720.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19080.48
            },
            {
              ""Measurement_GUID"": ""c94a889f-509c-4e3f-b092-305a84f7481e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 386954.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19110.96
            },
            {
              ""Measurement_GUID"": ""67e69499-a7f0-4949-a70a-45a2cd748dd9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 387221.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19141.44
            },
            {
              ""Measurement_GUID"": ""af99d6b1-0a32-4601-bc63-aa8cbb7a6680"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 387621.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19171.92
            },
            {
              ""Measurement_GUID"": ""fbafe8a3-623f-4d74-b5f5-dff0c2cb02d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 388188.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19202.4
            },
            {
              ""Measurement_GUID"": ""ba7f755e-37a8-48cb-94fb-f912720586f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 388455.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19232.88
            },
            {
              ""Measurement_GUID"": ""b0c23b82-f23a-4d5f-9306-0da7adfcb777"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 388722.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19263.36
            },
            {
              ""Measurement_GUID"": ""725204af-4a19-4de5-9bd8-f43af94749f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 389256.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19293.84
            },
            {
              ""Measurement_GUID"": ""9a011ae7-604e-4d28-9a8a-483b588add09"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 389623.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19324.32
            },
            {
              ""Measurement_GUID"": ""9908ab38-3eef-4729-ae28-ed1ac5033d85"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 389890.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19354.8
            },
            {
              ""Measurement_GUID"": ""7b7148c5-9966-4e8a-90fe-e518512622f0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 390157.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19385.28
            },
            {
              ""Measurement_GUID"": ""faab176a-709b-4cd5-aff5-75c4bf11bb4f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 390757.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19415.76
            },
            {
              ""Measurement_GUID"": ""83b677a1-b647-4b27-9b68-748cebed32bf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 391058.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19446.24
            },
            {
              ""Measurement_GUID"": ""2b47f987-0a8c-4b7a-afaa-3a0fbc98ee8e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 391225.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19476.72
            },
            {
              ""Measurement_GUID"": ""f919c291-079a-4477-bbde-99c34d2f85b7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 391692.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19507.2
            },
            {
              ""Measurement_GUID"": ""87dc86d3-7cd2-4574-97c0-b0ca706b6b42"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 392259.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19537.68
            },
            {
              ""Measurement_GUID"": ""5bfc66b3-fd5a-49e6-aeff-a4a2ff14fbe5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 392492.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19568.16
            },
            {
              ""Measurement_GUID"": ""57d98e93-4678-4a57-bcca-2b32a39f4877"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 392726.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19598.64
            },
            {
              ""Measurement_GUID"": ""cc976df6-6aa4-4598-981c-4cfa2240bff3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 393227.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19629.12
            },
            {
              ""Measurement_GUID"": ""aa3fe301-0b3d-4ba6-b8a0-4925e4dc28ff"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 393594.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19659.6
            },
            {
              ""Measurement_GUID"": ""26efab38-e697-4329-98c5-8ed1debcda7f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 393861.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19690.08
            },
            {
              ""Measurement_GUID"": ""162aeacb-fbb7-4643-8f4f-25f8d72082d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 394127.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19720.56
            },
            {
              ""Measurement_GUID"": ""36102828-5ff2-41b7-9cbe-4585b9135940"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 394528.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19751.04
            },
            {
              ""Measurement_GUID"": ""73e320d8-1ed3-40ef-80f8-07788796cb01"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 395195.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19781.52
            },
            {
              ""Measurement_GUID"": ""b0e9c2e6-e02d-46fb-93ac-8cd037d0d61a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 395596.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19812.0
            },
            {
              ""Measurement_GUID"": ""88f1ff83-ddfd-4738-9ece-b4f1477b40a8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 395796.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19842.48
            },
            {
              ""Measurement_GUID"": ""0eedc4aa-206e-4e28-8d88-b9d3476897eb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 396029.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19872.96
            },
            {
              ""Measurement_GUID"": ""0051f8c6-b51e-4af4-affa-2461bb52f590"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 396563.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19903.44
            },
            {
              ""Measurement_GUID"": ""64b99730-5b9b-4bda-83c5-52a387c63dea"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 397064.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19933.92
            },
            {
              ""Measurement_GUID"": ""535ec405-0634-4e34-acf3-24442bd27dc7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 397331.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19964.4
            },
            {
              ""Measurement_GUID"": ""f15e9ff6-e3c5-451e-937f-9254983643c8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 397531.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 19994.88
            },
            {
              ""Measurement_GUID"": ""05dff526-ca83-4f79-a36b-aa25ec297831"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 397798.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20025.36
            },
            {
              ""Measurement_GUID"": ""044a5fc1-9eb2-4310-9ab4-674ab4031a19"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 398198.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20055.84
            },
            {
              ""Measurement_GUID"": ""cd30a94e-49c8-4cbd-b148-44e96181a380"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 399032.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20086.32
            },
            {
              ""Measurement_GUID"": ""bd11e29c-d869-4963-b179-fda02df229c5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 399399.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20116.8
            },
            {
              ""Measurement_GUID"": ""48e9e45c-e4b7-487c-b276-1e6d1e7b4874"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 399566.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20147.28
            },
            {
              ""Measurement_GUID"": ""371e0549-2e32-444c-a54e-ff60651f6484"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 399766.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20177.76
            },
            {
              ""Measurement_GUID"": ""3cb589c3-a2d6-4eb2-8f9f-684be60da5f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 400067.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20208.24
            },
            {
              ""Measurement_GUID"": ""39ad8a18-5942-400c-9156-67a6eb98890e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 400467.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20238.72
            },
            {
              ""Measurement_GUID"": ""bb67a5fe-7cbd-473b-b684-fdee8892d7e0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 401201.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20269.2
            },
            {
              ""Measurement_GUID"": ""1d1d01ee-c2dc-4307-8ce3-93ecd4e8fa23"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 401435.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20299.68
            },
            {
              ""Measurement_GUID"": ""8180e97b-5f5f-45df-888c-fe93cd613708"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 401635.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20330.16
            },
            {
              ""Measurement_GUID"": ""9eee0381-7963-43a6-a896-d1e6fcfc6b8f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 401835.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20360.64
            },
            {
              ""Measurement_GUID"": ""bde0df0e-8d96-4e21-8a85-6cc1f4daa19f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 402302.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20391.12
            },
            {
              ""Measurement_GUID"": ""a4f04371-86dd-41af-ac63-cb4198779d89"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 402903.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20421.6
            },
            {
              ""Measurement_GUID"": ""8e7850b2-e297-4bcf-bf77-2eae0bbe15f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 403170.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20452.08
            },
            {
              ""Measurement_GUID"": ""815a1d15-d514-4cf8-ba98-37f07f20ba55"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 403403.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20482.56
            },
            {
              ""Measurement_GUID"": ""b657a2de-93dd-47f9-a9db-7930f9d5f481"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 403604.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20513.04
            },
            {
              ""Measurement_GUID"": ""b61caff2-8bd9-407e-b13a-e6484c39043d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 404171.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20543.52
            },
            {
              ""Measurement_GUID"": ""9c8ca5e2-281e-4b12-bb4d-386c8c19f96f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 404838.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20574.0
            },
            {
              ""Measurement_GUID"": ""3b03367d-0255-4e9d-907d-313e9f3beb1c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 405038.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20604.48
            },
            {
              ""Measurement_GUID"": ""1ca33949-bd86-4d60-8b80-33b8b3f7b05e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 405239.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20634.96
            },
            {
              ""Measurement_GUID"": ""a2eaa2ea-7037-407c-8972-2aa624ce50f5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 405572.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20665.44
            },
            {
              ""Measurement_GUID"": ""ee0a90fc-8215-42ad-9320-ebe0fc27510e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 405906.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20695.92
            },
            {
              ""Measurement_GUID"": ""7c46945c-81e1-4324-98cd-fa4c55fe0fab"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 406507.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20726.4
            },
            {
              ""Measurement_GUID"": ""cc81cd6c-95db-4989-9897-363fc813a4fb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 406773.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20756.88
            },
            {
              ""Measurement_GUID"": ""00856a07-f808-4a17-8de2-49e10dd9db90"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 406974.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20787.36
            },
            {
              ""Measurement_GUID"": ""5b9dca12-0842-4d20-88df-e9c807d6417f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 407341.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20817.84
            },
            {
              ""Measurement_GUID"": ""61bd96ee-a0a7-4e93-81dd-08b1c319414c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 408075.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20848.32
            },
            {
              ""Measurement_GUID"": ""1df0a660-24a2-462d-ac00-51edc5d39040"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 408275.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20878.8
            },
            {
              ""Measurement_GUID"": ""0b8969ee-ba06-4974-894b-fefc912db002"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 408475.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20909.28
            },
            {
              ""Measurement_GUID"": ""e088dd8c-e95a-4b4c-9939-d2aae2ea4c38"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 409009.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20939.76
            },
            {
              ""Measurement_GUID"": ""95e746f7-7c60-421e-afb6-ff2d386fd59c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 409376.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 20970.24
            },
            {
              ""Measurement_GUID"": ""ca47f55c-fa4c-46b0-bec7-0a971b4f5ae7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 409543.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21000.72
            },
            {
              ""Measurement_GUID"": ""9389731a-6d61-42af-8345-15c07036cb48"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 409843.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21031.2
            },
            {
              ""Measurement_GUID"": ""10078728-f353-4bd2-8b78-f4b84827530a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 410410.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21061.68
            },
            {
              ""Measurement_GUID"": ""551fab9d-3b3e-400a-a4b8-e1809e74cb3a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 410644.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21092.16
            },
            {
              ""Measurement_GUID"": ""ddc12af7-dd63-4510-a69c-0725b0e6471c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 410844.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21122.64
            },
            {
              ""Measurement_GUID"": ""aa2734f7-7348-4862-9c9d-7e3c41be6341"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 411211.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21153.12
            },
            {
              ""Measurement_GUID"": ""5f136b45-69a6-4ec1-b855-09fc0adfecb2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 411645.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21183.6
            },
            {
              ""Measurement_GUID"": ""d3fbc837-8c2b-4f62-9fe3-eec7ae035dec"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 411945.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21214.08
            },
            {
              ""Measurement_GUID"": ""6eef3c61-80e2-4ba8-9936-739b2b5283f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 412312.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21244.56
            },
            {
              ""Measurement_GUID"": ""39bb3476-cd7e-4955-8dcf-38fb996a2b5c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 412813.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21275.04
            },
            {
              ""Measurement_GUID"": ""755443d4-1230-4a26-b7c5-604113a1c38c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 413046.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21305.52
            },
            {
              ""Measurement_GUID"": ""f3b5f898-ba5d-40dd-adb6-97bba269aee4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 413413.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21336.0
            },
            {
              ""Measurement_GUID"": ""61a4e8c2-1c8b-4acf-9c49-3c7fbadbc773"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 413814.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21366.48
            },
            {
              ""Measurement_GUID"": ""2e88497e-1654-41b9-8200-e845b69f30a2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 414114.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21396.96
            },
            {
              ""Measurement_GUID"": ""726720dd-498b-4662-bb55-269a60c74fe8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 414414.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21427.44
            },
            {
              ""Measurement_GUID"": ""11cb2ea4-fdb4-4360-889c-4f82e851f883"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 414715.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21457.92
            },
            {
              ""Measurement_GUID"": ""d2e14dde-e739-497a-affc-a57c9009af5b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 415482.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21488.4
            },
            {
              ""Measurement_GUID"": ""ee7ac429-38e6-4b31-97e3-04e98daf5eab"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 415749.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21518.88
            },
            {
              ""Measurement_GUID"": ""c2ec4f6e-d142-4665-bdad-8fbca4df5984"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 415883.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21549.36
            },
            {
              ""Measurement_GUID"": ""27420969-2ccd-4dae-8174-66b882a33ec6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 416283.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21579.84
            },
            {
              ""Measurement_GUID"": ""a73d610a-5013-4b24-8005-f0bbaaa1eba5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 416850.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21610.32
            },
            {
              ""Measurement_GUID"": ""ae6747a5-6339-4b60-bd86-3c0ec4ecaeba"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 417050.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21640.8
            },
            {
              ""Measurement_GUID"": ""1ecd1b8a-0734-4c11-a5fb-d022ec357c00"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 417251.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21671.28
            },
            {
              ""Measurement_GUID"": ""7a52aa74-6632-41ee-8ccb-ee66c2e70175"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 417484.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21701.76
            },
            {
              ""Measurement_GUID"": ""99db0a23-aa3f-474a-a6dc-19164d9ea24e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 417818.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21732.24
            },
            {
              ""Measurement_GUID"": ""1bb0fcd4-661a-44f8-bc8d-fdbeeec1e929"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 418418.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21762.72
            },
            {
              ""Measurement_GUID"": ""1e984366-5a57-4949-88b7-cca77ed478c4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 418685.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21793.2
            },
            {
              ""Measurement_GUID"": ""ce6849ce-3e8d-41d1-b761-2dcfa6b53acd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 418886.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21823.68
            },
            {
              ""Measurement_GUID"": ""1f1ada61-e7d1-4d8f-aaf0-9511ac8c51a8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 419286.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21854.16
            },
            {
              ""Measurement_GUID"": ""0550e898-d74c-4658-9295-2ea999e32153"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 419953.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21884.64
            },
            {
              ""Measurement_GUID"": ""f81b8ac1-982d-406f-ae28-e12dfa226359"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 420287.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21915.12
            },
            {
              ""Measurement_GUID"": ""33c3b9c4-3ff3-4bba-80f6-a427b19921e0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 420487.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21945.6
            },
            {
              ""Measurement_GUID"": ""e01da837-8491-4711-9fbc-fda9058d938d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 420721.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 21976.08
            },
            {
              ""Measurement_GUID"": ""c241dea5-92aa-499d-a41d-8049104a61ea"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 421054.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22006.56
            },
            {
              ""Measurement_GUID"": ""b5ff165b-4cdf-41d1-baa3-af438a10f3f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 421655.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22037.04
            },
            {
              ""Measurement_GUID"": ""acacd2a0-13be-4ddc-b809-210a5d021350"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 421922.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22067.52
            },
            {
              ""Measurement_GUID"": ""cc4675dd-2c3b-4fe3-9554-1b55b4b24356"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 422155.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22098.0
            },
            {
              ""Measurement_GUID"": ""0f3518b9-5e67-427d-aaa4-fbf595fb2eac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 422422.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22128.48
            },
            {
              ""Measurement_GUID"": ""d28ccc1e-537d-4fed-8f92-0cb8e6e08dfc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 422923.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22158.96
            },
            {
              ""Measurement_GUID"": ""937eb22c-b827-49bc-9236-3a7efb8acc79"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 423357.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22189.44
            },
            {
              ""Measurement_GUID"": ""179ba420-2af4-4e86-a210-b408882ef642"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 423624.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22219.92
            },
            {
              ""Measurement_GUID"": ""a1af02b0-5f73-4889-8dbd-ab4eafc88861"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 423857.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22250.4
            },
            {
              ""Measurement_GUID"": ""f2c650c5-27a1-4803-962d-7ca7a5030ed0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 424057.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22280.88
            },
            {
              ""Measurement_GUID"": ""bc02eb10-b0cf-4782-8cc6-22864fef787b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 424324.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22311.36
            },
            {
              ""Measurement_GUID"": ""338468b1-ac62-4e83-9486-2588d7a1fde0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 424992.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22341.84
            },
            {
              ""Measurement_GUID"": ""38fa1b25-0245-4293-98be-2de98bdf5607"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 425526.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22372.32
            },
            {
              ""Measurement_GUID"": ""11f95ced-5de1-4056-9cf8-4f21717260e4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 425759.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22402.8
            },
            {
              ""Measurement_GUID"": ""aded4e91-9a99-4a3e-88c0-708218bb89b6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 425926.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22433.28
            },
            {
              ""Measurement_GUID"": ""7494f96c-5983-4f73-b582-aaddd72cc1ed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 426193.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22463.76
            },
            {
              ""Measurement_GUID"": ""38ab74c4-3ffc-4b85-a1db-03911ae0131b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 426493.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22494.24
            },
            {
              ""Measurement_GUID"": ""a2f05ec5-b1b2-4cef-a2fa-5366778ac542"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 427194.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22524.72
            },
            {
              ""Measurement_GUID"": ""a9db0ef4-091b-4df5-be37-3d8ccecc40f4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 427561.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22555.2
            },
            {
              ""Measurement_GUID"": ""66efe149-c6b5-4adf-9331-5efe5019c9e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 427694.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22585.68
            },
            {
              ""Measurement_GUID"": ""026ea9a7-8ad8-45f6-9eb9-2db591c8e8f0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 427895.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22616.16
            },
            {
              ""Measurement_GUID"": ""662f6c79-fe7f-4e03-8db3-c39a144146bd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 428428.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22646.64
            },
            {
              ""Measurement_GUID"": ""02706511-7427-4f91-8244-7afb4bdf3ff5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 428829.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22677.12
            },
            {
              ""Measurement_GUID"": ""3b7c036d-e437-41d8-b8c8-3ace7d277469"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 429162.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22707.6
            },
            {
              ""Measurement_GUID"": ""21f59516-3022-4ea7-8f5e-adb8a3269dc6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 429396.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22738.08
            },
            {
              ""Measurement_GUID"": ""240b5ecb-fd7f-4126-932d-d861c308dbd1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 429663.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22768.56
            },
            {
              ""Measurement_GUID"": ""ec359954-8948-4427-9d7f-55cc92baab16"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 430531.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22799.04
            },
            {
              ""Measurement_GUID"": ""fb6d0a86-4eca-401f-9ad9-7b37d7cf4042"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 430864.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22829.52
            },
            {
              ""Measurement_GUID"": ""c38e2252-c8a3-4732-bd86-2b3d064515ed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 431031.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22860.0
            },
            {
              ""Measurement_GUID"": ""0bf01240-1b5a-4f71-9f38-f769a4a5cc21"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 431198.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22890.48
            },
            {
              ""Measurement_GUID"": ""7f06fdc0-dacd-47bd-a621-923744d5d601"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 431798.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22920.96
            },
            {
              ""Measurement_GUID"": ""b5da10a8-aab3-4cf3-9cd4-b1d63b731606"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 432165.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22951.44
            },
            {
              ""Measurement_GUID"": ""9da8ce8b-9b3c-4f60-aa56-ae9ddb18d2e3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 432366.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 22981.92
            },
            {
              ""Measurement_GUID"": ""12715b84-92f3-4bc0-8fb7-23598a357566"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 432699.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23012.4
            },
            {
              ""Measurement_GUID"": ""a9b2d583-6e13-4351-8935-a7480d981bdd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 433267.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23042.88
            },
            {
              ""Measurement_GUID"": ""5a3c946f-7f26-4c10-bfc8-b773a20b27a8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 433500.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23073.36
            },
            {
              ""Measurement_GUID"": ""670f2f5f-f660-4c58-b825-5758d78c6298"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 433700.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23103.84
            },
            {
              ""Measurement_GUID"": ""321bd1a5-7e15-4c4e-a3a3-76eab812da71"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 434134.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23134.32
            },
            {
              ""Measurement_GUID"": ""0646cd93-f19c-4359-9b2a-5b4c9d30504f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 434601.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23164.8
            },
            {
              ""Measurement_GUID"": ""6d081ede-f4fc-4746-8f3c-ec53047696de"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 434835.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23195.28
            },
            {
              ""Measurement_GUID"": ""960e7c5e-728a-403c-bac1-27e7c15b1c84"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 435135.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23225.76
            },
            {
              ""Measurement_GUID"": ""a47ef65c-cca4-4950-a92e-510134ebe4de"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 435569.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23256.24
            },
            {
              ""Measurement_GUID"": ""f1abdc42-bf66-4379-a460-ba0f4dda2049"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 435869.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23286.72
            },
            {
              ""Measurement_GUID"": ""fb744b02-c448-4671-885a-a08cdd800124"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 436203.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23317.2
            },
            {
              ""Measurement_GUID"": ""22daa59d-c740-4a93-afac-d0af8b2f75a0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 436537.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23347.68
            },
            {
              ""Measurement_GUID"": ""3c931ec7-bf67-4509-bec2-c33303fca527"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 437070.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23378.16
            },
            {
              ""Measurement_GUID"": ""c9eec4e2-d1bb-4252-ae99-7d9e75bebea8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 437304.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23408.64
            },
            {
              ""Measurement_GUID"": ""9d86169f-2f29-480a-b263-32304a47d417"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 437571.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23439.12
            },
            {
              ""Measurement_GUID"": ""ece1b496-a151-408e-b20b-4444227827b1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 438405.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23469.6
            },
            {
              ""Measurement_GUID"": ""59aecb88-15f2-4816-bc72-814d055c242e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 438705.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23500.08
            },
            {
              ""Measurement_GUID"": ""e3841e2c-4ed4-4711-968b-0aabfb7dca25"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 439006.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23530.56
            },
            {
              ""Measurement_GUID"": ""e1ae33b7-161f-45ad-9460-21f4bf7ba6d4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 440407.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23561.04
            },
            {
              ""Measurement_GUID"": ""3b1fe39b-0c9a-4eb3-8157-f220f4541618"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 440674.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23591.52
            },
            {
              ""Measurement_GUID"": ""3a599bdd-e75a-4331-9bce-f7d40e1a2319"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 440908.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23622.0
            },
            {
              ""Measurement_GUID"": ""e6e4a329-72b3-4e1f-bb12-698e667ba378"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 441742.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23652.48
            },
            {
              ""Measurement_GUID"": ""14ecb132-fabd-4dbf-bafb-484d9e4ff272"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 442676.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23682.96
            },
            {
              ""Measurement_GUID"": ""07fcb166-9a70-48be-8d89-7c5608e1e47e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 442910.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23713.44
            },
            {
              ""Measurement_GUID"": ""e675119c-afb3-4825-9f92-171b4e81f1e9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 443777.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23743.92
            },
            {
              ""Measurement_GUID"": ""f36a5f03-a0d7-4e7d-a139-c0ea670fc3dd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 444912.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23774.4
            },
            {
              ""Measurement_GUID"": ""8136fc89-175b-4d19-a793-deb4c47b0f18"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 445279.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23804.88
            },
            {
              ""Measurement_GUID"": ""9928e51f-0e51-4305-983c-92a3a65a20be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 445512.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23835.36
            },
            {
              ""Measurement_GUID"": ""2d07e170-2f1b-466d-aafa-447f9769a8cf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 445812.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23865.84
            },
            {
              ""Measurement_GUID"": ""ff075226-ca67-4540-86ec-cf2384e47273"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 446446.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23896.32
            },
            {
              ""Measurement_GUID"": ""257ba443-d105-4929-aef0-57def539ae88"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 448215.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23926.8
            },
            {
              ""Measurement_GUID"": ""edb0ea8b-5bfb-4afe-bf7f-0a3e580c5405"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 449216.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23957.28
            },
            {
              ""Measurement_GUID"": ""532554cc-2d86-4148-9024-90df1d34dd0d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 480214.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 23987.76
            },
            {
              ""Measurement_GUID"": ""03ab1273-e728-498e-81df-8e7e68105255"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 481949.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24018.24
            },
            {
              ""Measurement_GUID"": ""ae12b44f-fe2e-4e46-86b3-63b4520174aa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 482649.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24048.72
            },
            {
              ""Measurement_GUID"": ""e9857e59-0647-4d3f-b67e-f7ee1d1bc8be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 482883.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24079.2
            },
            {
              ""Measurement_GUID"": ""e5360b92-7bcd-4c17-b005-f551544ab1f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 483050.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24109.68
            },
            {
              ""Measurement_GUID"": ""b72a044e-8c08-4487-aa44-86a830990323"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 483717.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24140.16
            },
            {
              ""Measurement_GUID"": ""2468e384-6612-42ad-91d7-3ff0c5c9649c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 484251.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24170.64
            },
            {
              ""Measurement_GUID"": ""2e1b29f5-fa38-4da8-bd3c-a211365d7307"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 484852.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24201.12
            },
            {
              ""Measurement_GUID"": ""2e6c11bd-2bfc-4c57-a4cc-681bd8fb0216"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 485118.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24231.6
            },
            {
              ""Measurement_GUID"": ""e25a0506-ed90-4b41-b3ab-263331e8816d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 485485.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24262.08
            },
            {
              ""Measurement_GUID"": ""87dc7369-a328-42e1-a82b-07517a525138"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 486153.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24292.56
            },
            {
              ""Measurement_GUID"": ""1111c621-9e69-4ab3-8529-90c6c158de6b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 486887.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24323.04
            },
            {
              ""Measurement_GUID"": ""85ec0dfe-a772-4444-b521-3100528f7598"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 487187.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24353.52
            },
            {
              ""Measurement_GUID"": ""ae612750-f28f-4bfa-8b0d-3eff5659a26d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 487354.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24384.0
            },
            {
              ""Measurement_GUID"": ""4d771268-3c64-4b6b-b37c-ee25803f7cea"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 487554.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24414.48
            },
            {
              ""Measurement_GUID"": ""0ae335ff-603a-452e-af1d-41fed5353aeb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 487788.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24444.96
            },
            {
              ""Measurement_GUID"": ""e21acaf8-a6e4-4231-a0e7-bbe5ba299264"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 488121.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24475.44
            },
            {
              ""Measurement_GUID"": ""d74c1f06-620d-4e6c-adf6-3c2291f1f230"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 488655.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24505.92
            },
            {
              ""Measurement_GUID"": ""31b5e844-9b69-4f4e-b1f7-b2fd747d3e4e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 488822.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24536.4
            },
            {
              ""Measurement_GUID"": ""54a0c0e6-224b-4aca-a6be-fa5c71932777"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 488989.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24566.88
            },
            {
              ""Measurement_GUID"": ""479e6694-e56d-4d0a-a701-bcbfcd0d527d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 489323.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24597.36
            },
            {
              ""Measurement_GUID"": ""3df4dbbd-235e-4998-a5d8-33a0b93b7df5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 489756.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24627.84
            },
            {
              ""Measurement_GUID"": ""8d9d4931-9b2f-43da-aa00-de0525cdb65a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 490090.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24658.32
            },
            {
              ""Measurement_GUID"": ""7a39b4e4-ad13-458a-a19a-b2bed886fb5e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 490257.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24688.8
            },
            {
              ""Measurement_GUID"": ""b5736730-2b76-4bb4-a424-59ea70e66ed3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 490457.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24719.28
            },
            {
              ""Measurement_GUID"": ""197e167a-fe9e-45ed-98a3-65bf1a9a1d7a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 490657.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24749.76
            },
            {
              ""Measurement_GUID"": ""b29bf860-4a00-404d-9e07-c0b42ce1d97e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 491091.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24780.24
            },
            {
              ""Measurement_GUID"": ""b2741549-2eab-422e-bbc1-0705c4126fb3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 491725.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24810.72
            },
            {
              ""Measurement_GUID"": ""5fa53ec0-59ba-4721-950d-f031f1b6307a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 491959.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24841.2
            },
            {
              ""Measurement_GUID"": ""21c25e2d-cd6d-4133-9014-72be8f54291a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 492125.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24871.68
            },
            {
              ""Measurement_GUID"": ""6028baf1-04d2-4cc2-8ae2-8a9fe3c279a4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 492359.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24902.16
            },
            {
              ""Measurement_GUID"": ""1bf0d331-0524-40e9-abfe-8e1d70a661a9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 492759.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24932.64
            },
            {
              ""Measurement_GUID"": ""ad99e73f-eb9d-499a-8714-b63d2db1cba6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 493126.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24963.12
            },
            {
              ""Measurement_GUID"": ""66e67433-bebd-4561-8fed-d1a6e70005bd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 493393.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 24993.6
            },
            {
              ""Measurement_GUID"": ""5d7e5d5f-d15f-41ed-a02f-fa1bbbee7dbc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 493594.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25024.08
            },
            {
              ""Measurement_GUID"": ""156280f9-7e1a-4613-bcdb-4cf3813519d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 493994.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25054.56
            },
            {
              ""Measurement_GUID"": ""e10c7421-f392-43e1-b695-6c073664790f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 494361.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25085.04
            },
            {
              ""Measurement_GUID"": ""82629cbc-97cd-474c-83d0-077dd72a5108"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 494528.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25115.52
            },
            {
              ""Measurement_GUID"": ""5ffb0b1f-48cb-45c1-b147-4ae5065051a7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 494795.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25146.0
            },
            {
              ""Measurement_GUID"": ""3d4cb466-64a9-49ad-a8fb-14672c3e2678"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 495395.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25176.48
            },
            {
              ""Measurement_GUID"": ""523c1b90-1b74-44ca-9f0a-b0825b7c3c3c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 495629.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25206.96
            },
            {
              ""Measurement_GUID"": ""b3fe547e-21ce-4c46-bc66-02bb453daa2a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 495796.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25237.44
            },
            {
              ""Measurement_GUID"": ""c9730c32-70b6-476c-b4e5-6d0557ac6219"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 496196.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25267.92
            },
            {
              ""Measurement_GUID"": ""71b9eaa5-9081-45f7-a59d-702b89886925"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 496463.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25298.4
            },
            {
              ""Measurement_GUID"": ""9a755984-82c5-4995-ad26-7217ccb28e5e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 496630.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25328.88
            },
            {
              ""Measurement_GUID"": ""51d8f1bd-6fa3-448c-9661-f6f6e8214b56"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 497064.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25359.36
            },
            {
              ""Measurement_GUID"": ""a5df113f-069a-41ba-95f4-9c1db7f41b1d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 497464.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25389.84
            },
            {
              ""Measurement_GUID"": ""e1b224a3-77c7-413f-9c96-63b1981bf4c9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 497631.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25420.32
            },
            {
              ""Measurement_GUID"": ""0b2bee19-c797-4410-9195-6a39178df808"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 497798.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25450.8
            },
            {
              ""Measurement_GUID"": ""453af4b3-fc7d-4f4b-94ff-da777f096b2a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 497998.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25481.28
            },
            {
              ""Measurement_GUID"": ""6cc8ea9a-4b1a-49b5-9772-62a2d9a97193"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 498432.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25511.76
            },
            {
              ""Measurement_GUID"": ""b0ffb86e-af30-4a0f-9a9d-f83f9e8f3946"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 498732.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25542.24
            },
            {
              ""Measurement_GUID"": ""3f790f64-a9c4-4128-8885-13100f258667"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 498999.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25572.72
            },
            {
              ""Measurement_GUID"": ""bd1574cb-0fe5-4242-9c02-36f0d786a7db"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 499266.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25603.2
            },
            {
              ""Measurement_GUID"": ""430631b2-fe6b-47a9-a157-aa8a6c51c29d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 499666.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25633.68
            },
            {
              ""Measurement_GUID"": ""fdff0819-18fe-4b55-b3bb-04ed712718b8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 499900.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25664.16
            },
            {
              ""Measurement_GUID"": ""0da5d203-e1f2-444f-920e-2c7ed23666f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 500067.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25694.64
            },
            {
              ""Measurement_GUID"": ""f2977a14-809b-471a-8597-9364e3566845"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 500434.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25725.12
            },
            {
              ""Measurement_GUID"": ""a34b03b0-e0f8-415d-b4b6-579c45a34e63"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 500934.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25755.6
            },
            {
              ""Measurement_GUID"": ""ce807edf-7459-4cd0-92ac-bc1568c6c186"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 501068.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25786.08
            },
            {
              ""Measurement_GUID"": ""5341d366-f225-46fd-9a15-933259b6c37f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 501235.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25816.56
            },
            {
              ""Measurement_GUID"": ""06f902d5-455d-40c6-85ae-129e19a8c2b0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 501735.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25847.04
            },
            {
              ""Measurement_GUID"": ""cb8e2309-7f78-4411-be9c-c62b332a18e4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 502069.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25877.52
            },
            {
              ""Measurement_GUID"": ""7819a534-3851-4020-82b8-5a516bd1d993"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 502302.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25908.0
            },
            {
              ""Measurement_GUID"": ""43f32a99-61dd-43f1-b932-4912c51f8b5d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 502536.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25938.48
            },
            {
              ""Measurement_GUID"": ""2b8a866d-aeac-40f2-ac6b-dbc83f8ecabd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 502669.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25968.96
            },
            {
              ""Measurement_GUID"": ""99402998-2d65-41e4-b722-8d4477ff27e0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 503003.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 25999.44
            },
            {
              ""Measurement_GUID"": ""8474b1a9-8ed3-4c56-8b02-c56dffa1412f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 503604.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26029.92
            },
            {
              ""Measurement_GUID"": ""3a5c915a-78cf-4fcb-be23-a2c217041885"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 503837.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26060.4
            },
            {
              ""Measurement_GUID"": ""7bae7a76-aa64-451e-9bbc-b4d6cfd1779d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 503971.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26090.88
            },
            {
              ""Measurement_GUID"": ""0cc05ffd-ed7d-44a0-99eb-9c52dd50f247"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 504171.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26121.36
            },
            {
              ""Measurement_GUID"": ""93fef334-7fac-46f4-b2b6-d4391cd604c6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 504438.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26151.84
            },
            {
              ""Measurement_GUID"": ""869debe9-5278-4b6e-af2d-a0876da5c904"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 504805.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26182.32
            },
            {
              ""Measurement_GUID"": ""78dcbe8c-712f-4a79-8e79-825e67ad2d00"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 505239.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26212.8
            },
            {
              ""Measurement_GUID"": ""d81fa5cf-a4b5-4308-8fd7-88bb768dac77"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 505439.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26243.28
            },
            {
              ""Measurement_GUID"": ""490f99d1-13b2-426a-aee9-69db285fe7ae"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 505606.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26273.76
            },
            {
              ""Measurement_GUID"": ""a587aa7b-9501-4943-9376-d8946579d8a6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 505906.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26304.24
            },
            {
              ""Measurement_GUID"": ""cee41df3-1536-440f-abde-987916007de6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 506273.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26334.72
            },
            {
              ""Measurement_GUID"": ""6f8ca972-370b-431e-a54b-d06bc66e7a95"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 506540.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26365.2
            },
            {
              ""Measurement_GUID"": ""65cb6a8a-24b0-44e1-93c4-7490daa0cf45"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 506907.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26395.68
            },
            {
              ""Measurement_GUID"": ""68621102-25d9-4100-8c51-6407cee8b88e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 507107.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26426.16
            },
            {
              ""Measurement_GUID"": ""3b4e9f6c-9630-4dd2-b015-81c3948e597b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 507341.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26456.64
            },
            {
              ""Measurement_GUID"": ""9c5cb61c-b39d-4db2-9267-036dd1df8a66"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 507674.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26487.12
            },
            {
              ""Measurement_GUID"": ""623b9eea-db6a-407f-8501-cade61184420"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 508108.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26517.6
            },
            {
              ""Measurement_GUID"": ""93cf247d-2fee-481a-bdcf-9f617678a24b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 508442.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26548.08
            },
            {
              ""Measurement_GUID"": ""8ef37bf6-53ba-4952-b63e-297e9d1be0d6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 508642.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26578.56
            },
            {
              ""Measurement_GUID"": ""c77cc7f0-c8a7-4f19-9b72-95923e85e3cd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 508842.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26609.04
            },
            {
              ""Measurement_GUID"": ""30f7dd4c-146c-46fd-baaa-b27502f4084f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 509109.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26639.52
            },
            {
              ""Measurement_GUID"": ""9762a637-d16f-4de0-87a9-1ddf12b4ca30"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 509610.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26670.0
            },
            {
              ""Measurement_GUID"": ""4ec44ce0-9f8f-4a30-a5ae-f8a91be021ce"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 510077.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26700.48
            },
            {
              ""Measurement_GUID"": ""28745245-02bf-4692-bb53-ea59469b0195"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 510277.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26730.96
            },
            {
              ""Measurement_GUID"": ""01d433d5-e98a-4270-bcfc-b597391dc2f9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 510410.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26761.44
            },
            {
              ""Measurement_GUID"": ""28ca8776-a1d0-4d49-915f-61c8f4215a86"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 510611.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26791.92
            },
            {
              ""Measurement_GUID"": ""977d8757-e09f-4dc9-8539-6b549d8284dc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 510811.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26822.4
            },
            {
              ""Measurement_GUID"": ""e459eedf-ac40-41fe-b323-d7799049beeb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 511278.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26852.88
            },
            {
              ""Measurement_GUID"": ""01fbaeef-e8ca-40af-a5eb-7ce24ee51f10"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 511745.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26883.36
            },
            {
              ""Measurement_GUID"": ""02569c4a-c0d8-4f1f-9e5e-4b64a4af24b0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 511912.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26913.84
            },
            {
              ""Measurement_GUID"": ""222c3cf8-46b8-4a1b-b371-2c926ab4d37c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 512079.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26944.32
            },
            {
              ""Measurement_GUID"": ""8b45fd4b-ed99-498b-9b8b-c0f1c2cc1c56"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 512546.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 26974.8
            },
            {
              ""Measurement_GUID"": ""2dce017a-7a6e-487e-8791-cce516a9909c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 512913.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27005.28
            },
            {
              ""Measurement_GUID"": ""2b4f8aa9-6857-40f4-b43e-45d401e3cf9f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 513180.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27035.76
            },
            {
              ""Measurement_GUID"": ""68fd492a-45c1-46fc-8a02-4e5883bdde07"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 513413.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27066.24
            },
            {
              ""Measurement_GUID"": ""fa26d562-1fb7-4fe3-924d-0009fdfa9ead"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 513614.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27096.72
            },
            {
              ""Measurement_GUID"": ""9d8a2e9b-897f-43e7-bbb6-77409945e7ee"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 513881.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27127.2
            },
            {
              ""Measurement_GUID"": ""b095e0cc-e3ed-420f-b2de-669f53dbd8d7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 514448.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27157.68
            },
            {
              ""Measurement_GUID"": ""e5921fd4-afc1-4325-ad60-1015d46536bb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 514648.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27188.16
            },
            {
              ""Measurement_GUID"": ""562fe1a2-4763-4fb1-9a4c-19b796a6e7a7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 514848.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27218.64
            },
            {
              ""Measurement_GUID"": ""3502caf7-2308-4b95-bd59-8ec6aa1c2b22"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 515215.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27249.12
            },
            {
              ""Measurement_GUID"": ""c128b875-6e53-4a93-ad5b-558c3618278e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 515616.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27279.6
            },
            {
              ""Measurement_GUID"": ""932dbc99-3590-4cf5-8741-4949eff9ec3a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 515816.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27310.08
            },
            {
              ""Measurement_GUID"": ""2dbd3dc1-f5b0-4d05-90fe-38fd97884993"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 516116.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27340.56
            },
            {
              ""Measurement_GUID"": ""0fb48db6-8aae-4632-9c0f-cf598556735b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 516416.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27371.04
            },
            {
              ""Measurement_GUID"": ""9072dd80-d4b2-4c29-80e1-d881fb320a8c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 516717.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27401.52
            },
            {
              ""Measurement_GUID"": ""ce70897d-3b3e-4f82-ab21-055bd8c1eee4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 517150.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27432.0
            },
            {
              ""Measurement_GUID"": ""de72ca4e-179d-43d6-a371-8ce2895c795d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 517384.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27462.48
            },
            {
              ""Measurement_GUID"": ""537a5b56-b22b-4e94-b939-7c10fb979abc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 517851.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27492.96
            },
            {
              ""Measurement_GUID"": ""17368a23-e459-4aad-90f0-9fc0d17784e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 518051.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27523.44
            },
            {
              ""Measurement_GUID"": ""20a0f652-71ab-4338-b788-f24c8e903a42"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 518385.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27553.92
            },
            {
              ""Measurement_GUID"": ""b16787ef-caee-453e-b7e0-9eb6f78fe9a6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 518785.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27584.4
            },
            {
              ""Measurement_GUID"": ""273fbfd3-0183-4cb6-ae55-0a24c43c461f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 519052.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27614.88
            },
            {
              ""Measurement_GUID"": ""2a80f67d-f46e-4ac0-811d-e825ce75451e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 519586.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27645.36
            },
            {
              ""Measurement_GUID"": ""11a79ae6-9514-4a7c-b043-11989e43a05e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 519887.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27675.84
            },
            {
              ""Measurement_GUID"": ""e7f657a1-a0ea-4f9c-928b-748ca219981f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 520087.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27706.32
            },
            {
              ""Measurement_GUID"": ""6e5a14fb-911e-4c28-b3fb-56a04a036105"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 520487.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27736.8
            },
            {
              ""Measurement_GUID"": ""e3f43557-42a6-46bc-ace2-b780c3513a68"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 520854.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27767.28
            },
            {
              ""Measurement_GUID"": ""26cfd946-c4e6-46e3-a2d4-a91263622973"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 521088.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27797.76
            },
            {
              ""Measurement_GUID"": ""0de43814-b72a-4fea-8118-269571ba124d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 521388.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27828.24
            },
            {
              ""Measurement_GUID"": ""a658acc8-aa61-40aa-a68a-c14ea42da222"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 521722.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27858.72
            },
            {
              ""Measurement_GUID"": ""262a5186-6891-4b22-9634-0916dac9e536"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 521955.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27889.2
            },
            {
              ""Measurement_GUID"": ""a9a71e78-b732-4ccc-9a80-f8997dfdc641"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 522189.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27919.68
            },
            {
              ""Measurement_GUID"": ""10e68e90-03a5-4c56-b346-c2be30be85dd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 522456.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27950.16
            },
            {
              ""Measurement_GUID"": ""c24b33bf-fb6c-414b-a0b6-fd341159ed93"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 522923.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 27980.64
            },
            {
              ""Measurement_GUID"": ""3e2b87ed-c136-4009-afe7-00881011b901"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 523223.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28011.12
            },
            {
              ""Measurement_GUID"": ""f1c01ca2-2160-4651-8693-2ec1720b0e4a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 523423.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28041.6
            },
            {
              ""Measurement_GUID"": ""619584d6-6d08-493d-a900-84b6d6d3234b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 523857.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28072.08
            },
            {
              ""Measurement_GUID"": ""2b285a2a-d826-4675-9d0b-c97aed1cbe6f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 524191.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28102.56
            },
            {
              ""Measurement_GUID"": ""1978e413-e55e-4805-9bf4-eb9e15bca677"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 524358.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28133.04
            },
            {
              ""Measurement_GUID"": ""450bbc98-a70e-4059-954e-d9f6745ccf13"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 524658.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28163.52
            },
            {
              ""Measurement_GUID"": ""6410bd14-e613-42e3-a8cf-ad5e7add4d7d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 524958.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28194.0
            },
            {
              ""Measurement_GUID"": ""814ff6be-12f3-4ea7-8fe6-60cdc16a5bbc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 525125.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28224.48
            },
            {
              ""Measurement_GUID"": ""8ec084ff-1479-4043-82e9-1dcd269f05fd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 525359.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28254.96
            },
            {
              ""Measurement_GUID"": ""e6821b28-72f4-4dd9-a98e-8739a12d43e7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 525759.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28285.44
            },
            {
              ""Measurement_GUID"": ""f723bc1a-232e-4a13-aae9-c68b16e159f3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 525993.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28315.92
            },
            {
              ""Measurement_GUID"": ""4e1847a3-2932-4f5c-adb4-429f99a2e9cf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 526226.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28346.4
            },
            {
              ""Measurement_GUID"": ""2420964a-1849-4078-acab-1f060687fb2e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 526793.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28376.88
            },
            {
              ""Measurement_GUID"": ""90efe4c6-1ab7-470b-8abc-14cf68e3b905"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 527094.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28407.36
            },
            {
              ""Measurement_GUID"": ""374376b8-9b47-4f91-a23c-d9ab44a5e1a7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 527227.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28437.84
            },
            {
              ""Measurement_GUID"": ""dad52fa2-5137-4852-b60d-6d54615a8188"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 527427.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28468.32
            },
            {
              ""Measurement_GUID"": ""4f8c4ca8-08b9-4325-87eb-a2bff5629a85"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 527895.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28498.8
            },
            {
              ""Measurement_GUID"": ""e176c442-7e76-4f4c-954d-f9dfb57b9dd2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 528095.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28529.28
            },
            {
              ""Measurement_GUID"": ""121e98fe-f128-4bb8-82aa-f1db9082f96f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 528562.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28559.76
            },
            {
              ""Measurement_GUID"": ""2c880729-7bb2-4d8b-a180-6bbe2333126a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 528996.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28590.24
            },
            {
              ""Measurement_GUID"": ""93ac6800-1d95-4edd-956d-35b2ab15571e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 529162.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28620.72
            },
            {
              ""Measurement_GUID"": ""695ffd52-f14d-441e-8db1-0152a21d27d3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 529396.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28651.2
            },
            {
              ""Measurement_GUID"": ""4af3b69d-84d4-419c-a1b5-90f7634a3032"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 529763.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28681.68
            },
            {
              ""Measurement_GUID"": ""21dd569e-e64c-4e5c-8e45-313e6d38e0c3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 529997.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28712.16
            },
            {
              ""Measurement_GUID"": ""5e9c5a63-2edd-46a7-bba3-b05ec4a53181"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 530497.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28742.64
            },
            {
              ""Measurement_GUID"": ""797e3f1e-0b27-4b7c-93a7-21dcdcdb3cbb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 530731.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28773.12
            },
            {
              ""Measurement_GUID"": ""d5875b07-f936-41b7-b121-105ddb303a29"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 530964.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28803.6
            },
            {
              ""Measurement_GUID"": ""2ccf49b3-45f7-4768-8eba-7267a8d8997a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 531331.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28834.08
            },
            {
              ""Measurement_GUID"": ""b1a72b93-ae60-452b-be3d-336778d63644"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 531765.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28864.56
            },
            {
              ""Measurement_GUID"": ""cad9659e-fa72-47c2-b80d-fc4bb1887ffc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 531999.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28895.04
            },
            {
              ""Measurement_GUID"": ""2072c39a-d4d5-4680-8ab2-79d688488d5b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 532299.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28925.52
            },
            {
              ""Measurement_GUID"": ""3120cba0-15da-4e8e-a1c9-c8fd038757eb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 532799.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28956.0
            },
            {
              ""Measurement_GUID"": ""b1ad61e7-445e-4248-9979-5341f7f55eb9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 533033.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 28986.48
            },
            {
              ""Measurement_GUID"": ""998bdbd9-0353-41f8-9aea-ada5a28b47aa"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 533267.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29016.96
            },
            {
              ""Measurement_GUID"": ""71f57678-b6e1-4982-9615-342f3c804179"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 533800.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29047.44
            },
            {
              ""Measurement_GUID"": ""7c11e56b-4e88-484c-8853-47ef57b11907"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 534001.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29077.92
            },
            {
              ""Measurement_GUID"": ""f4362f2a-264a-4b0c-9c34-3235cf9ce164"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 534168.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29108.4
            },
            {
              ""Measurement_GUID"": ""50b3349c-8fc0-408b-a2d1-94b8bf981528"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 534568.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29138.88
            },
            {
              ""Measurement_GUID"": ""d2df4af5-8f74-4166-93d6-a7d979800c7e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 534902.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29169.36
            },
            {
              ""Measurement_GUID"": ""b9d35fa6-62cc-4b5a-a877-bca6ea1b908a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 535269.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29199.84
            },
            {
              ""Measurement_GUID"": ""8f84ea97-06a0-40d1-ae7b-3af6701f584b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 535536.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29230.32
            },
            {
              ""Measurement_GUID"": ""30e24640-cf89-481d-889f-e47b3d8f01ac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 535936.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29260.8
            },
            {
              ""Measurement_GUID"": ""0b42d315-182e-4084-bfc6-7b7ef6b6a667"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 536270.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29291.28
            },
            {
              ""Measurement_GUID"": ""c3000f9a-31e5-47ff-9edc-b9e64e58f416"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 537004.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29321.76
            },
            {
              ""Measurement_GUID"": ""d0f875c0-d8f4-4e8e-92b6-733ce9e0fe5f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 537304.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29352.24
            },
            {
              ""Measurement_GUID"": ""4fdebf7d-e657-49c2-814d-8a52c0bda2e2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 537504.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29382.72
            },
            {
              ""Measurement_GUID"": ""81e1e6d5-ffde-45cd-b8f3-5725da4442a0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 538038.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29413.2
            },
            {
              ""Measurement_GUID"": ""6c2f2c52-8639-41ec-8210-2571586409d9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 538338.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29443.68
            },
            {
              ""Measurement_GUID"": ""723f7008-59d4-4871-8d0a-5f91f7069298"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 538739.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29474.16
            },
            {
              ""Measurement_GUID"": ""6bf4abd8-d1c3-4310-9aa9-29263fc874ed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 539072.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29504.64
            },
            {
              ""Measurement_GUID"": ""16c05642-395a-4cdb-98b4-2dda2a8974f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 539373.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29535.12
            },
            {
              ""Measurement_GUID"": ""e9667b1d-9f16-44b2-a1cb-c28d874624d5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 539740.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29565.6
            },
            {
              ""Measurement_GUID"": ""1b7e8522-95a1-4fda-a96b-3459285b422c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 540007.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29596.08
            },
            {
              ""Measurement_GUID"": ""768608db-1509-4688-a1da-04c963b499d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 540274.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29626.56
            },
            {
              ""Measurement_GUID"": ""6733cc22-9911-415a-9cb4-7842de42e50f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 540674.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29657.04
            },
            {
              ""Measurement_GUID"": ""a347ce2a-d83d-4f32-8a87-9e5439c4d694"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 541041.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29687.52
            },
            {
              ""Measurement_GUID"": ""c7515608-3522-4204-a141-2631b8188a61"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 541408.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29718.0
            },
            {
              ""Measurement_GUID"": ""8deba8a3-196c-4c9c-96f5-2336b3ce99d1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 541708.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29748.48
            },
            {
              ""Measurement_GUID"": ""08c2c4b4-d58b-4bfe-8474-7d042ed92146"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 541975.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29778.96
            },
            {
              ""Measurement_GUID"": ""908d5c2c-03f0-4dcc-8d30-9b10496b1330"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 542209.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29809.44
            },
            {
              ""Measurement_GUID"": ""a2b1d7af-6dcd-461b-95e9-3c268d33a854"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 542376.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29839.92
            },
            {
              ""Measurement_GUID"": ""9f33009e-cf64-4459-abf1-e050f040cca7"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 542676.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29870.4
            },
            {
              ""Measurement_GUID"": ""32c690da-9fe0-40bb-8a57-01d7da6a4a1a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 543143.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29900.88
            },
            {
              ""Measurement_GUID"": ""366d19ee-f3a7-476e-91bd-6c14e9f6247c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 543477.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29931.36
            },
            {
              ""Measurement_GUID"": ""bdada013-866d-4a20-b23a-57c852f6a381"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 543710.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29961.84
            },
            {
              ""Measurement_GUID"": ""2543fc19-dd95-403e-a4fc-c1043c1683d6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 544044.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 29992.32
            },
            {
              ""Measurement_GUID"": ""4a589233-0279-447d-b153-b9c2d4bdd879"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 544378.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30022.8
            },
            {
              ""Measurement_GUID"": ""497ece21-bb5b-4170-b080-cd7e10249991"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 544711.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30053.28
            },
            {
              ""Measurement_GUID"": ""5352d9ce-803a-4dc3-b490-9d8d80ef01d3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 545078.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30083.76
            },
            {
              ""Measurement_GUID"": ""4ad924d2-2269-4b8b-a867-5cfbc0c542d8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 545312.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30114.24
            },
            {
              ""Measurement_GUID"": ""4919d774-26a1-42b1-8268-dc2a740b1952"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 545712.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30144.72
            },
            {
              ""Measurement_GUID"": ""05489ad2-f3b5-4c3f-b485-b4d412deb5d2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 546113.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30175.2
            },
            {
              ""Measurement_GUID"": ""91006bbc-61a9-4343-aac5-ba9f579d5b1b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 546246.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30205.68
            },
            {
              ""Measurement_GUID"": ""67c587a0-5dc3-4d20-b0a8-f7cf0c2bb4c0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 546547.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30236.16
            },
            {
              ""Measurement_GUID"": ""f60f5f2f-7626-4434-be97-71ac4c6e6585"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 546914.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30266.64
            },
            {
              ""Measurement_GUID"": ""73ef7379-ca93-40ec-84ba-ebaef5ebc3e8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 547214.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30297.12
            },
            {
              ""Measurement_GUID"": ""be52e431-86ea-44ce-b294-8421fc3848f5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 547514.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30327.6
            },
            {
              ""Measurement_GUID"": ""600a510c-8aac-4790-9a8b-29e38e94c2b5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 547881.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30358.08
            },
            {
              ""Measurement_GUID"": ""5f5e0beb-916f-4011-a267-72d820c3f8f8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 548115.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30388.56
            },
            {
              ""Measurement_GUID"": ""00518ad7-c4be-4f31-9a7d-92f4ecb1c807"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 548282.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30419.04
            },
            {
              ""Measurement_GUID"": ""fcf5189c-f766-4865-8d45-8c642abaeba3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 548749.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30449.52
            },
            {
              ""Measurement_GUID"": ""0e1251d6-cf82-4a1d-a4a6-23abd27cc76d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 549183.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30480.0
            },
            {
              ""Measurement_GUID"": ""766aad08-e257-4a63-9224-50f062203f50"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 549383.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30510.48
            },
            {
              ""Measurement_GUID"": ""9e17a021-fed4-451b-91cd-1f8717e7944f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 549616.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30540.96
            },
            {
              ""Measurement_GUID"": ""3be08edf-cb75-47af-9971-3032b0c73ffe"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 549883.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30571.44
            },
            {
              ""Measurement_GUID"": ""9590f779-e633-432e-9192-ebed0f03fd64"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 550083.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30601.92
            },
            {
              ""Measurement_GUID"": ""de5c1f40-605b-432d-acbf-c87a41f8642c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 550517.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30632.4
            },
            {
              ""Measurement_GUID"": ""003ef977-93cf-4bd3-ad92-3667b6c0395b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 550918.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30662.88
            },
            {
              ""Measurement_GUID"": ""67fa43e6-ec0d-47f3-ba49-f910e602f635"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 551151.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30693.36
            },
            {
              ""Measurement_GUID"": ""9c60187c-2aef-4343-946c-5c0f125d35f2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 551485.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30723.84
            },
            {
              ""Measurement_GUID"": ""dc1dc51a-f901-439b-863f-48bd15d4e869"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 551919.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30754.32
            },
            {
              ""Measurement_GUID"": ""53bb53b2-42ab-4af1-82b9-8184f644617e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 552219.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30784.8
            },
            {
              ""Measurement_GUID"": ""cd2879d1-627b-45e2-bd50-19288d2f96be"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 552419.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30815.28
            },
            {
              ""Measurement_GUID"": ""8b20c0fd-1d59-4fec-9815-d0935845c8d9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 552753.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30845.76
            },
            {
              ""Measurement_GUID"": ""9d4aa794-efaf-4bf0-9590-7ea4eba3f512"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 552986.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30876.24
            },
            {
              ""Measurement_GUID"": ""fe9e5041-7843-4b0f-ad29-6a53e11570d4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 553320.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30906.72
            },
            {
              ""Measurement_GUID"": ""9e400f8e-0e4d-43ec-abbe-22e474950a68"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 553720.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30937.2
            },
            {
              ""Measurement_GUID"": ""a56a1f8a-586e-45d8-8222-80e94e4cbe29"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 553887.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30967.68
            },
            {
              ""Measurement_GUID"": ""fd96e49c-34ea-4be7-8c87-eab639f5ab7a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 554288.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 30998.16
            },
            {
              ""Measurement_GUID"": ""0cfe44b4-1329-431b-a39f-be997ba8a43d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 554655.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31028.64
            },
            {
              ""Measurement_GUID"": ""fda57e5e-8056-4f1b-b174-720a1cb5bc13"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 554788.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31059.12
            },
            {
              ""Measurement_GUID"": ""9fc7d91b-f4ef-4e6a-8bf4-9610ead9d120"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 555088.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31089.6
            },
            {
              ""Measurement_GUID"": ""9d533d90-bf32-41c8-8d6c-9b77ca5d72bc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 555489.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31120.08
            },
            {
              ""Measurement_GUID"": ""0064b928-ba6a-4916-b4c3-7ee80085a6da"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 555722.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31150.56
            },
            {
              ""Measurement_GUID"": ""5271c17d-f523-48ad-8cc1-0677b643831a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 556123.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31181.04
            },
            {
              ""Measurement_GUID"": ""d981ed21-a8d6-43cc-9fbf-d577615609bd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 556456.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31211.52
            },
            {
              ""Measurement_GUID"": ""a1a36b5b-60e8-458e-b80c-a0773e664655"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 556657.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31242.0
            },
            {
              ""Measurement_GUID"": ""395d51d8-ab90-471a-badc-385de97b5dbc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 556957.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31272.48
            },
            {
              ""Measurement_GUID"": ""fc8a6ae0-4972-49ce-af59-2851ab93976e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 557224.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31302.96
            },
            {
              ""Measurement_GUID"": ""d86e3f1b-b3f1-449f-b9a1-fa5b57baa724"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 557491.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31333.44
            },
            {
              ""Measurement_GUID"": ""881b2460-95c0-4622-b816-4f7643174067"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 557858.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31363.92
            },
            {
              ""Measurement_GUID"": ""396e590f-83b0-42cc-a54e-72d0d20164ef"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 558158.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31394.4
            },
            {
              ""Measurement_GUID"": ""a94a7b96-d840-4d0e-a838-3b83948e617a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 558425.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31424.88
            },
            {
              ""Measurement_GUID"": ""bc2ea62d-83fc-4e99-bd81-698dcab81248"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 558725.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31455.36
            },
            {
              ""Measurement_GUID"": ""ea04a554-a7b0-4e83-81e1-e5194f1f4738"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 559059.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31485.84
            },
            {
              ""Measurement_GUID"": ""d29459cb-815f-462b-8808-ddedbac06697"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 559326.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31516.32
            },
            {
              ""Measurement_GUID"": ""197954c0-b298-4cf9-a199-df2f2a82f627"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 559660.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31546.8
            },
            {
              ""Measurement_GUID"": ""886a548e-78b6-4598-b941-c96f30508098"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 559993.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31577.28
            },
            {
              ""Measurement_GUID"": ""56979d60-0972-4ab9-a372-1f17d8512ef6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 560227.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31607.76
            },
            {
              ""Measurement_GUID"": ""20db3ad9-a96c-4ddf-805c-32b38a623df8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 560460.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31638.24
            },
            {
              ""Measurement_GUID"": ""65a07b71-8c56-4044-bab1-c770714c4be6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 560794.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31668.72
            },
            {
              ""Measurement_GUID"": ""b455c4ed-32f9-4240-b30d-c53db9ce105f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 561261.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31699.2
            },
            {
              ""Measurement_GUID"": ""229f1bc5-0fdc-4531-a1af-3dabced188ed"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 561628.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31729.68
            },
            {
              ""Measurement_GUID"": ""c3301ca7-2ced-479b-95cf-3748bcc0e80c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 561895.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31760.16
            },
            {
              ""Measurement_GUID"": ""004b6c4f-b6cf-4e59-bb50-e11b7e1a3d24"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 562062.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31790.64
            },
            {
              ""Measurement_GUID"": ""66631aef-a9c7-4eda-8bc9-dc5755b81317"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 562262.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31821.12
            },
            {
              ""Measurement_GUID"": ""3d18b22f-e4f9-47fc-b106-80b16ed8c4f1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 562529.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31851.6
            },
            {
              ""Measurement_GUID"": ""0fd604ce-05d4-4961-9d4c-5c7faf939701"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 562829.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31882.08
            },
            {
              ""Measurement_GUID"": ""f2460638-5327-49ca-8cdd-ad82135bbfaf"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 563096.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31912.56
            },
            {
              ""Measurement_GUID"": ""c6f14cb6-e195-42da-b1d4-bb240e6800d8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 563664.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31943.04
            },
            {
              ""Measurement_GUID"": ""0263a742-f667-4bea-9d03-dad3c39a7362"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 563897.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 31973.52
            },
            {
              ""Measurement_GUID"": ""fa828713-2683-4182-ac20-908d584e15ca"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 564064.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32004.0
            },
            {
              ""Measurement_GUID"": ""e06e931c-8e30-420b-8ce0-3cb50409a722"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 564398.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32034.48
            },
            {
              ""Measurement_GUID"": ""004e3407-d173-4b1e-842d-accf16254b40"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 564698.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32064.96
            },
            {
              ""Measurement_GUID"": ""0915c33b-9a3e-42bf-954b-6a9729679b30"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 564965.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32095.44
            },
            {
              ""Measurement_GUID"": ""5c7c26a0-d34d-410c-8465-fb38253a9e09"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 565566.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32125.92
            },
            {
              ""Measurement_GUID"": ""c0d020d1-9066-494c-b2bd-6ac0b1a50ef2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 565866.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32156.4
            },
            {
              ""Measurement_GUID"": ""4d00bb03-820e-4d85-a571-2c58935268b4"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 566066.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32186.88
            },
            {
              ""Measurement_GUID"": ""c18bf84a-084e-4963-925f-4d2e4aa2d8e9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 566466.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32217.36
            },
            {
              ""Measurement_GUID"": ""1db0a963-94c3-4b37-a122-5ab39deded7b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 566733.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32247.84
            },
            {
              ""Measurement_GUID"": ""687ce6a8-41ff-4b45-9ce1-9d29e6ea5862"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 567000.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32278.32
            },
            {
              ""Measurement_GUID"": ""930d7694-3b2c-4cd9-a49c-09362d87b7bc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 567401.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32308.8
            },
            {
              ""Measurement_GUID"": ""13294dbf-194b-47a8-bace-10f83c450812"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 567901.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32339.28
            },
            {
              ""Measurement_GUID"": ""68bbc872-cf51-4f16-b420-af248e2d9599"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 568068.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32369.76
            },
            {
              ""Measurement_GUID"": ""fbf1200f-5528-49b4-9952-54b602d15abe"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 568268.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32400.24
            },
            {
              ""Measurement_GUID"": ""fede96c8-75f2-457d-9be6-b4d2aedf354d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 568669.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32430.72
            },
            {
              ""Measurement_GUID"": ""09947bbc-7f28-40cc-88ee-2dc77d24d238"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 568936.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32461.2
            },
            {
              ""Measurement_GUID"": ""2f94a664-e6c8-4f36-b38c-e952aa7e5c23"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 569136.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32491.68
            },
            {
              ""Measurement_GUID"": ""dcd7d7d2-da5a-4e4f-80ff-e40991615ea2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 569837.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32522.16
            },
            {
              ""Measurement_GUID"": ""58a6d2dd-9b22-4d00-8710-7448c4937d34"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 570204.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32552.64
            },
            {
              ""Measurement_GUID"": ""7e13ae66-d776-40fb-baa4-4373e1c9347a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 570437.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32583.12
            },
            {
              ""Measurement_GUID"": ""0dfb5139-0bca-494b-940e-64632066ff0c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 570637.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32613.6
            },
            {
              ""Measurement_GUID"": ""ff1efdbf-074e-45d6-ac99-da0697a1cc03"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 571004.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32644.08
            },
            {
              ""Measurement_GUID"": ""bb5a65df-b519-4dff-8bc2-8d2edd3869c8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 571505.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32674.56
            },
            {
              ""Measurement_GUID"": ""22ff99ff-3447-435e-914c-521c51af3009"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 571738.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32705.04
            },
            {
              ""Measurement_GUID"": ""7c954ed9-264d-4a51-a3a0-11d45883e9b9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 571972.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32735.52
            },
            {
              ""Measurement_GUID"": ""da2def41-db95-493d-ac12-12ba15ba57b6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 572239.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32766.0
            },
            {
              ""Measurement_GUID"": ""6010731e-6924-4bf1-879a-bb262b586cac"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 572573.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32796.48
            },
            {
              ""Measurement_GUID"": ""c8ee2274-f997-4089-92a9-5c02fcdaf83e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 572906.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32826.96
            },
            {
              ""Measurement_GUID"": ""ba49ab19-d757-4969-9463-20e5f6e9e3e0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 573073.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32857.44
            },
            {
              ""Measurement_GUID"": ""282a1c03-cada-4cd4-830d-22f3aff7622d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 573674.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32887.92
            },
            {
              ""Measurement_GUID"": ""40942949-aa2f-4ccb-9d97-78724c16db26"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 573974.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32918.4
            },
            {
              ""Measurement_GUID"": ""76e44414-70a1-43aa-9132-5b23d8268b49"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 574141.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32948.88
            },
            {
              ""Measurement_GUID"": ""1e08ad60-1cbb-424d-828f-5ec1447fc501"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 574308.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 32979.36
            },
            {
              ""Measurement_GUID"": ""487bd389-847c-41f1-9b97-77b7dfa24fe1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 574641.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33009.84
            },
            {
              ""Measurement_GUID"": ""9285cd99-4379-41b2-880c-4f07fdf174e5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 574942.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33040.32
            },
            {
              ""Measurement_GUID"": ""a62db29e-4346-477c-b72d-651e3b10520c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 575142.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33070.8
            },
            {
              ""Measurement_GUID"": ""8ac58026-3e77-4901-af8c-345170553ce0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 575509.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33101.28
            },
            {
              ""Measurement_GUID"": ""a20477ce-7e29-4390-afb1-4f87e224f528"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 575809.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33131.76
            },
            {
              ""Measurement_GUID"": ""3e4a138d-6dcf-4cc2-a88a-1112227c414f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 576510.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33162.24
            },
            {
              ""Measurement_GUID"": ""b54762a9-8828-43f9-9d8f-e895335364e8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 576877.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33192.72
            },
            {
              ""Measurement_GUID"": ""9a7e2223-0e27-4fe5-b253-1300ff376961"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 577077.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33223.2
            },
            {
              ""Measurement_GUID"": ""becfe833-fb96-4fc7-8389-9363748eaa80"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 577244.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33253.68
            },
            {
              ""Measurement_GUID"": ""3028afca-fdf1-404f-9127-a00b7dc76657"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 577411.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33284.16
            },
            {
              ""Measurement_GUID"": ""b16293c1-8aa4-4e9f-b143-6ac2b230a82a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 577778.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33314.64
            },
            {
              ""Measurement_GUID"": ""8a923117-d8df-4d2b-a32b-9b89169e750e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 578212.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33345.12
            },
            {
              ""Measurement_GUID"": ""7176dd7f-3e23-4362-bf75-4dac77e234a2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 578478.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33375.6
            },
            {
              ""Measurement_GUID"": ""9a0d31cb-f4b7-428e-8590-8672b1f99d08"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 578645.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33406.08
            },
            {
              ""Measurement_GUID"": ""bb08b83d-cb02-483b-b262-212124847bcd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 578946.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33436.56
            },
            {
              ""Measurement_GUID"": ""fe815e56-12c7-4942-b990-2c50050ffde5"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 579246.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33467.04
            },
            {
              ""Measurement_GUID"": ""d6a577b5-51f4-45a2-952c-cf18e615c083"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 579646.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33497.52
            },
            {
              ""Measurement_GUID"": ""a26e645b-4b8c-44c6-93ab-80af011c8463"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 580047.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33528.0
            },
            {
              ""Measurement_GUID"": ""494638fa-20bc-4d35-a543-f7b064c4560e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 580247.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33558.48
            },
            {
              ""Measurement_GUID"": ""e0cf8ca5-1dbe-4ca4-b592-2c209021635a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 580747.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33588.96
            },
            {
              ""Measurement_GUID"": ""16762d93-737e-4a71-9e91-7c350068a1b2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 581048.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33619.44
            },
            {
              ""Measurement_GUID"": ""e4c92060-592f-4cd8-8263-dff17d7e912e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 581248.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33649.92
            },
            {
              ""Measurement_GUID"": ""bc8ae12c-869b-4f00-ab39-6de2ae96e626"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 581548.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33680.4
            },
            {
              ""Measurement_GUID"": ""e0c171e8-a080-4ee3-88f7-3e352992735b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 581915.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33710.88
            },
            {
              ""Measurement_GUID"": ""9eb47e27-3b4c-4afd-8727-83debe0a78e3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 582482.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33741.36
            },
            {
              ""Measurement_GUID"": ""3a63652c-a4c2-43dc-8737-56700d7d3e52"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 582716.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33771.84
            },
            {
              ""Measurement_GUID"": ""0b32d7be-cbfe-4da1-8877-70ec0f3a0d2a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 582883.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33802.32
            },
            {
              ""Measurement_GUID"": ""0fa832ea-ec3e-4f20-9900-b21f0988f0ad"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 583350.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33832.8
            },
            {
              ""Measurement_GUID"": ""9ee3ab46-9bc9-45f2-96fe-b95c195808ef"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 583584.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33863.28
            },
            {
              ""Measurement_GUID"": ""24068f56-652e-4a42-834d-76fd2e75f62e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 583784.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33893.76
            },
            {
              ""Measurement_GUID"": ""fd64ff09-0c3b-4560-99b7-ddad3176f5e6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 584084.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33924.24
            },
            {
              ""Measurement_GUID"": ""c876fb55-e6a1-4727-8b77-ed2231d3cfe9"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 584351.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33954.72
            },
            {
              ""Measurement_GUID"": ""8e28b1cf-5387-413a-bd97-219a1b4efb75"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 584685.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 33985.2
            },
            {
              ""Measurement_GUID"": ""49510ef4-34ca-4212-ac5f-7d0d8ba3c962"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 585219.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34015.68
            },
            {
              ""Measurement_GUID"": ""0c027dd4-869f-4f5f-ae20-6d5e0f2d4eeb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 585452.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34046.16
            },
            {
              ""Measurement_GUID"": ""619b1415-7320-430e-ab48-1c46ee8f034b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 585719.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34076.64
            },
            {
              ""Measurement_GUID"": ""9d37e982-cc0f-407e-b4cc-d6ecb9cbe3eb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 586053.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34107.12
            },
            {
              ""Measurement_GUID"": ""c87a1bc0-416e-49cd-a74a-77fe76f4937b"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 586320.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34137.6
            },
            {
              ""Measurement_GUID"": ""e6b2fd7d-d95c-4d9a-b580-5feb668efb9d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 586687.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34168.08
            },
            {
              ""Measurement_GUID"": ""bc7c6a56-76ee-4f91-937c-9c593db000df"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 587087.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34198.56
            },
            {
              ""Measurement_GUID"": ""ab30aa7d-fc3b-4015-9efe-e6f465d7c8a6"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 587421.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34229.04
            },
            {
              ""Measurement_GUID"": ""69bd2575-793c-41d4-b3ea-7c0e2e6d3e6c"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 587654.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34259.52
            },
            {
              ""Measurement_GUID"": ""45d5d18d-32f6-4132-8951-372c9e6e6159"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 587955.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34290.0
            },
            {
              ""Measurement_GUID"": ""72c99579-764b-4609-a0aa-06b8ca3db09d"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 588589.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34320.48
            },
            {
              ""Measurement_GUID"": ""e96863a9-2019-498e-96c1-862dc0b88cdd"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 588922.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34350.96
            },
            {
              ""Measurement_GUID"": ""11e64cc0-d8b2-4ce1-a6cb-d58eb167bb59"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 589089.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34381.44
            },
            {
              ""Measurement_GUID"": ""83447211-3128-47be-befe-79920fbe923e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 589289.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34411.92
            },
            {
              ""Measurement_GUID"": ""54fec9ca-2c07-434c-8416-1711c938b84a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 589623.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34442.4
            },
            {
              ""Measurement_GUID"": ""bb5318de-a7f5-4ba5-b020-284a4f4d4e64"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 589957.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34472.88
            },
            {
              ""Measurement_GUID"": ""23282c02-38d0-469e-8186-5bba6c8e0927"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 590457.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34503.36
            },
            {
              ""Measurement_GUID"": ""680fede6-9695-4668-a4ac-682f5f75f940"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 590724.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34533.84
            },
            {
              ""Measurement_GUID"": ""aedcbfd1-1c1b-413c-92d8-a846ad43e52f"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 590924.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34564.32
            },
            {
              ""Measurement_GUID"": ""071b39ff-7c37-4464-86f5-ddae2974afcb"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 591158.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34594.8
            },
            {
              ""Measurement_GUID"": ""e3eac5ab-505f-434f-9de8-80a0aa2cacd1"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 591725.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34625.28
            },
            {
              ""Measurement_GUID"": ""b6e749c3-f68d-4cf6-8ce5-3cf179337567"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 592092.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34655.76
            },
            {
              ""Measurement_GUID"": ""bf66c7fd-06d7-44a9-8f3e-085a030f13e3"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 592292.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34686.24
            },
            {
              ""Measurement_GUID"": ""e2df5aa0-90d1-417d-b46b-991a5f385541"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 592559.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34716.72
            },
            {
              ""Measurement_GUID"": ""7d248784-4225-4318-9657-14d46774b220"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 592893.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34747.2
            },
            {
              ""Measurement_GUID"": ""8da639aa-df08-4b8e-a202-92fdc0a04ece"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 593393.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34777.68
            },
            {
              ""Measurement_GUID"": ""75a1c09f-9a8e-412e-abf7-8fad50d84d63"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 593627.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34808.16
            },
            {
              ""Measurement_GUID"": ""29e656a5-2b41-4b6b-8846-9d7b6ddce79a"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 593827.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34838.64
            },
            {
              ""Measurement_GUID"": ""d0838cac-1092-413e-8955-c4dd211eb070"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 594094.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34869.12
            },
            {
              ""Measurement_GUID"": ""ca00a089-cdd7-44f1-bda7-a1c321c876ca"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 594428.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34899.6
            },
            {
              ""Measurement_GUID"": ""485999a2-4fd5-497d-9530-d83d0f4ea3c8"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 594895.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34930.08
            },
            {
              ""Measurement_GUID"": ""567ccbf7-ccec-47a0-9027-c69a73ada480"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 595195.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34960.56
            },
            {
              ""Measurement_GUID"": ""fe161c91-04ef-4e2b-a649-bd08df7a2327"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 595462.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 34991.04
            },
            {
              ""Measurement_GUID"": ""fa17579a-a180-4bb4-b756-5b676b3860dc"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 596063.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35021.52
            },
            {
              ""Measurement_GUID"": ""b97bb655-7a86-4318-9012-a219cc241fae"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 596263.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35052.0
            },
            {
              ""Measurement_GUID"": ""f7356452-44e4-44af-b196-71e231fdd8c0"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 596597.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35082.48
            },
            {
              ""Measurement_GUID"": ""1542dfc0-b367-4afa-9a50-318ad124d0d2"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 597197.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35112.96
            },
            {
              ""Measurement_GUID"": ""c1c40ada-dc01-432d-90ca-2cc3911ca650"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 597664.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35143.44
            },
            {
              ""Measurement_GUID"": ""5774a361-a8e0-4157-8a12-91272409f878"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 605205.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35173.92
            },
            {
              ""Measurement_GUID"": ""18de3e57-8b0e-4a2e-9271-3e9bfcb0e906"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 605606.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35204.4
            },
            {
              ""Measurement_GUID"": ""b29646ef-843c-4c4f-861e-d05dcc49b995"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 605906.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 35234.88
            },
            {
              ""Measurement_GUID"": ""4b22e12b-904d-4de9-9c55-ba5ff7bd636e"",
              ""Media_GUID"": ""15ca6375-bf61-479e-ba92-9c79cf319392"",
              ""Time"": 647147.0,
              ""I_002_GUID"": ""968f3aa0-bfeb-4ae0-b555-1321ee27fa25"",
              ""Distance"": 0.0
            }
          ]
        }
      ]
    }
  ]
}
";

        var ele = JsonSerializer.Deserialize<JsonElement>(inspectionJSON);

        ////return this.Ok(
        ////    JsonSerializer.Deserialize<JsonElement>(
        ////        System.IO.File.ReadAllText("cool.json")));

        return this.Ok(new Manifest()
        {
            Id = id,
            AdditionalProperties = new()
            {
                { "what", 9 },
                { "blamo", null },
            },
            DeliverableName = "Spring 2023",
            IndividualNASSCOExchangeGenerate = true,
            CombinedNASSCOExchangeGenerate = true,
            CombinedReportIds = new[] {
                new Guid("74168c6c-b5f3-4a66-ba87-ec30a86c8fed"),
                new Guid("72F03502-C8C6-485B-BE64-30CD1F339796"),
                new Guid("a3fdbee0-9b49-4346-b5c7-202221ad8e89"),
            },
            IndividualReportIds = new[] { new Guid("74168c6c-b5f3-4a66-ba87-ec30a86c8fed") },
            Inspections = new JsonElement[] { ele },
        });
    }
}
