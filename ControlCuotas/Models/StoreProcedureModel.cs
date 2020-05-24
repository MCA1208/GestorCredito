using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlCuotas.Models
{
    public class StoreProcedureModel
    {

        public class SPName
        {
            public string spUserLogin       = "spUserLogin";


            //CLIENT
            public string spCreateClient    = "spCreateClient";
            public string spGetAllClient    = "spGetAllClient";
            public string spGetComboZona    = "spGetComboZona";
            public string spGetClientById   = "spGetClientById";
            public string spModifyClient    = "spModifyClient";
            public string spGetClientByDNI  = "spGetClientByDNI";
            public string spDeleteClient    = "spDeleteClient";

            //ZONE
            public string spGetAllZone      = "spGetAllZone";
            public string spAddZone         = "spAddZone";
            public string spGetZoneById     = "spGetZoneById";
            public string spModifyZone      = "spModifyZone";
            public string spDeleteZone      = "spDeleteZone";

            //PRESTAMO
            public string spAddPrestamo     = "spAddPrestamo";
            public string spGetAllPrestamo  = "spGetAllPrestamo";
            public string spGetPrestamoDetailById = "spGetPrestamoDetailById";
            public string spGetCuotaDetail = "spGetCuotaDetail";
            public string spSaveCuotaForId = "spSaveCuotaForId";
            public string spGetPrestamoForId = "spGetPrestamoForId";
            public string spSavePrestamoForId = "spSavePrestamoForId";
            public string spDeletePrestamo = "spDeletePrestamo";
            public string spExistPrestamo = "spExistPrestamo";


            //REPORT
            public string spGetReportPrincipal = "spGetReportPrincipal";
            public string spReportCuponStatus = "spReportCuponStatus";
            public string spReportInvestmentAndProfit = "spReportInvestmentAndProfit";
            public string spGetReportCuponByClient = "spGetReportCuponByClient";
            public string spReportSummaryClient = "spReportSummaryClient";
            public string spReportSummaryDetail = "spReportSummaryDetail";
            public string spReportCobranza = "spReportCobranza";
            public string spReportIrregularPayment ="spReportIrregularPayment";

        }
    }
}