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

            //ZONE
            public string spGetAllZone      = "spGetAllZone";
            public string spAddZone         = "spAddZone";
            public string spGetZoneById     = "spGetZoneById";
            public string spModifyZone      = "spModifyZone";

            //PRESTAMO
            public string spAddPrestamo     = "spAddPrestamo";
            public string spGetAllPrestamo  = "spGetAllPrestamo";
            public string spGetPrestamoDetailById = "spGetPrestamoDetailById";
            public string spChangeEstatusCuota = "spChangeEstatusCuota";
            public string spGetCuotaDetail = "spGetCuotaDetail";
            public string spSaveCuotaForId = "spSaveCuotaForId";

            //REPORT
            public string spGetReportPrincipal = "spGetReportPrincipal";
            public string spReportCuponStatus = "spReportCuponStatus";



        }
    }
}