using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)

			ImportProcesses();
            ImportMarketDocument();
            ImportPeriod();
            ImportPoint();
            ImportBidTimeSeries();
            ImportMeasurementPoint();


            //ImportBaseVoltages();
            //ImportLocations();
            //ImportPowerTransformers();
            //ImportTransformerWindings();
            //ImportWindingTests();

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import

		private void ImportProcesses()
        {
			SortedDictionary<string, object> cimProcesses = concreteModel.GetAllObjectsOfType("FTN.Process");
            if (cimProcesses != null)
            {
                foreach (KeyValuePair<string, object> cimProcessPair in cimProcesses)
                {
                    FTN.Process cimProcess = cimProcessPair.Value as FTN.Process;

                    ResourceDescription rd = CreateProcessResourceDescription(cimProcess);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Process ID = ").Append(cimProcess.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Process ID = ").Append(cimProcess.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateProcessResourceDescription(FTN.Process cimProcess)
        {
            ResourceDescription rd = null;
            if (cimProcess != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PROCESS, importHelper.CheckOutIndexForDMSType(DMSType.PROCESS));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimProcess.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateProcessProperties(cimProcess, rd);
            }
            return rd;
        }

        private void ImportMarketDocument()
        {
            SortedDictionary<string, object> cimMarketDocuments = concreteModel.GetAllObjectsOfType("FTN.MarketDocument");
            if (cimMarketDocuments != null)
            {
                foreach (KeyValuePair<string, object> cimMarketDocumentPair in cimMarketDocuments)
                {
                    FTN.MarketDocument cimMarketDocument = cimMarketDocumentPair.Value as FTN.MarketDocument;

                    ResourceDescription rd = CreateMarketDocumentResourceDescription(cimMarketDocument);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("MarketDocument ID = ").Append(cimMarketDocument.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("MarketDocument ID = ").Append(cimMarketDocument.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateMarketDocumentResourceDescription(FTN.MarketDocument cimMarketDocument)
        {
            ResourceDescription rd = null;
            if (cimMarketDocument != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.MARKETDOCUMENT, importHelper.CheckOutIndexForDMSType(DMSType.MARKETDOCUMENT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimMarketDocument.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateMarketDocumentProperties(cimMarketDocument, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportPeriod()
        {
            SortedDictionary<string, object> cimPeriods = concreteModel.GetAllObjectsOfType("FTN.Period");
            if (cimPeriods != null)
            {
                foreach (KeyValuePair<string, object> cimPeriodPair in cimPeriods)
                {
                    FTN.Period cimPeriod = cimPeriodPair.Value as FTN.Period;

                    ResourceDescription rd = CreatePeriodResourceDescription(cimPeriod);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Period ID = ").Append(cimPeriod.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Period ID = ").Append(cimPeriod.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatePeriodResourceDescription(FTN.Period cimPeriod)
        {
            ResourceDescription rd = null;
            if (cimPeriod != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PERIOD, importHelper.CheckOutIndexForDMSType(DMSType.PERIOD));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimPeriod.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulatePeriodProperties(cimPeriod, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportPoint()
        {
            SortedDictionary<string, object> cimPoints = concreteModel.GetAllObjectsOfType("FTN.Point");
            if (cimPoints != null)
            {
                foreach (KeyValuePair<string, object> cimPointPair in cimPoints)
                {
                    FTN.Point cimPoint = cimPointPair.Value as FTN.Point;

                    ResourceDescription rd = CreatePointResourceDescription(cimPoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Point ID = ").Append(cimPoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Point ID = ").Append(cimPoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatePointResourceDescription(FTN.Point cimPoint)
        {
            ResourceDescription rd = null;
            if (cimPoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POINT, importHelper.CheckOutIndexForDMSType(DMSType.POINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimPoint.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulatePointProperties(cimPoint, rd, importHelper, report);
            }
            return rd;
        }
        
        private void ImportBidTimeSeries()
        {
            SortedDictionary<string, object> cimBidTimeSerieses = concreteModel.GetAllObjectsOfType("FTN.BidTimeSeries");
            if (cimBidTimeSerieses != null)
            {
                foreach (KeyValuePair<string, object> cimBidTimeSeriesPair in cimBidTimeSerieses)
                {
                    FTN.BidTimeSeries cimBidTimeSeries = cimBidTimeSeriesPair.Value as FTN.BidTimeSeries;

                    ResourceDescription rd = CreatecimBidTimeSeriesResourceDescription(cimBidTimeSeries);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("BidTimeSeries ID = ").Append(cimBidTimeSeries.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("BidTimeSeries ID = ").Append(cimBidTimeSeries.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatecimBidTimeSeriesResourceDescription(FTN.BidTimeSeries cimBidTimeSeries)
        {
            ResourceDescription rd = null;
            if (cimBidTimeSeries != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BIDTIMESERIES, importHelper.CheckOutIndexForDMSType(DMSType.BIDTIMESERIES));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimBidTimeSeries.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateBidTimeSeriesProperties(cimBidTimeSeries, rd, importHelper, report);
            }
            return rd;
        }


        private void ImportMeasurementPoint()
        {
            SortedDictionary<string, object> cimMeasurementPoints = concreteModel.GetAllObjectsOfType("FTN.MeasurementPoint");
            if (cimMeasurementPoints != null)
            {
                foreach (KeyValuePair<string, object> cimMeasurementPointPair in cimMeasurementPoints)
                {
                    FTN.MeasurementPoint cimMeasurementPoint = cimMeasurementPointPair.Value as FTN.MeasurementPoint;

                    ResourceDescription rd = CreatecimMeasurementPointResourceDescription(cimMeasurementPoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("MeasurementPoint ID = ").Append(cimMeasurementPoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("MeasurementPoint ID = ").Append(cimMeasurementPoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreatecimMeasurementPointResourceDescription(FTN.MeasurementPoint cimMeasurementPoint)
        {
            ResourceDescription rd = null;
            if (cimMeasurementPoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.MEASUREMENTPOINT, importHelper.CheckOutIndexForDMSType(DMSType.MEASUREMENTPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimMeasurementPoint.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateMeasurementPointProperties(cimMeasurementPoint, rd, importHelper, report);
            }
            return rd;
        }


        //private void ImportBaseVoltages()
        //{
        //	SortedDictionary<string, object> cimBaseVoltages = concreteModel.GetAllObjectsOfType("FTN.BaseVoltage");
        //	if (cimBaseVoltages != null)
        //	{
        //		foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseVoltages)
        //		{
        //			FTN.BaseVoltage cimBaseVoltage = cimBaseVoltagePair.Value as FTN.BaseVoltage;

        //			ResourceDescription rd = CreateBaseVoltageResourceDescription(cimBaseVoltage);
        //			if (rd != null)
        //			{
        //				delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
        //				report.Report.Append("BaseVoltage ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
        //			}
        //			else
        //			{
        //				report.Report.Append("BaseVoltage ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
        //			}
        //		}
        //		report.Report.AppendLine();
        //	}
        //}

        //private ResourceDescription CreateBaseVoltageResourceDescription(FTN.BaseVoltage cimBaseVoltage)
        //{
        //	ResourceDescription rd = null;
        //	if (cimBaseVoltage != null)
        //	{
        //		long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BASEVOLTAGE, importHelper.CheckOutIndexForDMSType(DMSType.BASEVOLTAGE));
        //		rd = new ResourceDescription(gid);
        //		importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

        //		////populate ResourceDescription
        //		PowerTransformerConverter.PopulateBaseVoltageProperties(cimBaseVoltage, rd);
        //	}
        //	return rd;
        //}

        //private void ImportLocations()
        //{
        //	SortedDictionary<string, object> cimLocations = concreteModel.GetAllObjectsOfType("FTN.Location");
        //	if (cimLocations != null)
        //	{
        //		foreach (KeyValuePair<string, object> cimLocationPair in cimLocations)
        //		{
        //			FTN.Location cimLocation = cimLocationPair.Value as FTN.Location;

        //			ResourceDescription rd = CreateLocationResourceDescription(cimLocation);
        //			if (rd != null)
        //			{
        //				delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
        //				report.Report.Append("Location ID = ").Append(cimLocation.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
        //			}
        //			else
        //			{
        //				report.Report.Append("Location ID = ").Append(cimLocation.ID).AppendLine(" FAILED to be converted");
        //			}
        //		}
        //		report.Report.AppendLine();
        //	}
        //}

        //private ResourceDescription CreateLocationResourceDescription(FTN.Location cimLocation)
        //{
        //	ResourceDescription rd = null;
        //	if (cimLocation != null)
        //	{
        //		long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.LOCATION, importHelper.CheckOutIndexForDMSType(DMSType.LOCATION));
        //		rd = new ResourceDescription(gid);
        //		importHelper.DefineIDMapping(cimLocation.ID, gid);

        //		////populate ResourceDescription
        //		PowerTransformerConverter.PopulateLocationProperties(cimLocation, rd);
        //	}
        //	return rd;
        //}

        //private void ImportPowerTransformers()
        //{
        //	SortedDictionary<string, object> cimPowerTransformers = concreteModel.GetAllObjectsOfType("FTN.PowerTransformer");
        //	if (cimPowerTransformers != null)
        //	{
        //		foreach (KeyValuePair<string, object> cimPowerTransformerPair in cimPowerTransformers)
        //		{
        //			FTN.PowerTransformer cimPowerTransformer = cimPowerTransformerPair.Value as FTN.PowerTransformer;

        //			ResourceDescription rd = CreatePowerTransformerResourceDescription(cimPowerTransformer);
        //			if (rd != null)
        //			{
        //				delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
        //				report.Report.Append("PowerTransformer ID = ").Append(cimPowerTransformer.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
        //			}
        //			else
        //			{
        //				report.Report.Append("PowerTransformer ID = ").Append(cimPowerTransformer.ID).AppendLine(" FAILED to be converted");
        //			}
        //		}
        //		report.Report.AppendLine();
        //	}
        //}

        //private ResourceDescription CreatePowerTransformerResourceDescription(FTN.PowerTransformer cimPowerTransformer)
        //{
        //	ResourceDescription rd = null;
        //	if (cimPowerTransformer != null)
        //	{
        //		long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTR, importHelper.CheckOutIndexForDMSType(DMSType.POWERTR));
        //		rd = new ResourceDescription(gid);
        //		importHelper.DefineIDMapping(cimPowerTransformer.ID, gid);

        //		////populate ResourceDescription
        //		PowerTransformerConverter.PopulatePowerTransformerProperties(cimPowerTransformer, rd, importHelper, report);
        //	}
        //	return rd;
        //}

        //private void ImportTransformerWindings()
        //{
        //	SortedDictionary<string, object> cimTransformerWindings = concreteModel.GetAllObjectsOfType("FTN.TransformerWinding");
        //	if (cimTransformerWindings != null)
        //	{
        //		foreach (KeyValuePair<string, object> cimTransformerWindingPair in cimTransformerWindings)
        //		{
        //			FTN.TransformerWinding cimTransformerWinding = cimTransformerWindingPair.Value as FTN.TransformerWinding;

        //			ResourceDescription rd = CreateTransformerWindingResourceDescription(cimTransformerWinding);
        //			if (rd != null)
        //			{
        //				delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
        //				report.Report.Append("TransformerWinding ID = ").Append(cimTransformerWinding.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
        //			}
        //			else
        //			{
        //				report.Report.Append("TransformerWinding ID = ").Append(cimTransformerWinding.ID).AppendLine(" FAILED to be converted");
        //			}
        //		}
        //		report.Report.AppendLine();
        //	}
        //}

        //private ResourceDescription CreateTransformerWindingResourceDescription(FTN.TransformerWinding cimTransformerWinding)
        //{
        //	ResourceDescription rd = null;
        //	if (cimTransformerWinding != null)
        //	{
        //		long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTRWINDING, importHelper.CheckOutIndexForDMSType(DMSType.POWERTRWINDING));
        //		rd = new ResourceDescription(gid);
        //		importHelper.DefineIDMapping(cimTransformerWinding.ID, gid);

        //		////populate ResourceDescription
        //		PowerTransformerConverter.PopulateTransformerWindingProperties(cimTransformerWinding, rd, importHelper, report);
        //	}
        //	return rd;
        //}

        //private void ImportWindingTests()
        //{
        //	SortedDictionary<string, object> cimWindingTests = concreteModel.GetAllObjectsOfType("FTN.WindingTest");
        //	if (cimWindingTests != null)
        //	{
        //		foreach (KeyValuePair<string, object> cimWindingTestPair in cimWindingTests)
        //		{
        //			FTN.WindingTest cimWindingTest = cimWindingTestPair.Value as FTN.WindingTest;

        //			ResourceDescription rd = CreateWindingTestResourceDescription(cimWindingTest);
        //			if (rd != null)
        //			{
        //				delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
        //				report.Report.Append("WindingTest ID = ").Append(cimWindingTest.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
        //			}
        //			else
        //			{
        //				report.Report.Append("WindingTest ID = ").Append(cimWindingTest.ID).AppendLine(" FAILED to be converted");
        //			}
        //		}
        //		report.Report.AppendLine();
        //	}
        //}

        //private ResourceDescription CreateWindingTestResourceDescription(FTN.WindingTest cimWindingTest)
        //{
        //	ResourceDescription rd = null;
        //	if (cimWindingTest != null)
        //	{
        //		long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.WINDINGTEST, importHelper.CheckOutIndexForDMSType(DMSType.WINDINGTEST));
        //		rd = new ResourceDescription(gid);
        //		importHelper.DefineIDMapping(cimWindingTest.ID, gid);

        //		////populate ResourceDescription
        //		PowerTransformerConverter.PopulateWindingTestProperties(cimWindingTest, rd, importHelper, report);
        //	}
        //	return rd;
        //}
        #endregion Import
    }
}

