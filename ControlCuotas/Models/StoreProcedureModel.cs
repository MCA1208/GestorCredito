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
            public string spReportQuotaPaid = "spReportQuotaPaid";

            //VENDOR
            public string spGetAllVendor = "spGetAllVendor";
            public string spAddVendor = "spAddVendor";
            public string spModifyVendor = "spModifyVendor";
            public string spDeleteVendor = "spDeleteVendor";
            public string spGetVendorById = "spGetVendorById";

            //MARK
            public string spGetAllMark = "spGetAllMark";
            public string spAddMark = "spAddMark";
            public string spModifyMark = "spModifyMark";
            public string spDeleteMark = "spDeleteMark";
            public string spGetMarkById = "spGetMarkById";

            //TYPEPRODUCT
            public string spGetAllTypeProduct = "spGetAllTypeProduct";
            public string spAddTypeProduct = "spAddTypeProduct";
            public string spModifyTypeProduct = "spModifyTypeProduct";
            public string spDeleteTypeProduct = "spDeleteTypeProduct";
            public string spGetTypeProductById = "spGetTypeProductById";

            //PRODUCT
            public string spGetAllProduct = "spGetAllProduct";
            public string spAddProduct = "spAddProduct";
            public string spModifyProduct = "spModifyProduct";
            public string spDeleteProduct = "spDeleteProduct";
            public string spGetProductById = "spGetProductById";

            //SALE
            public string spAddSale = "spAddSale";
            public string spAddSaleDetail = "spAddSaleDetail";
            public string spAddSaleQuota = "spAddSaleQuota";
            public string spAddSaleError = "spAddSaleError";
            public string spGetSaleDetailById = "spGetSaleDetailById";
            public string spGetSaleById = "spGetSaleById";
            public string spSaveSaleById = "spSaveSaleById";
            public string spGetProductCuotaDetail = "spGetProductCuotaDetail";
            public string spSaveProductCuotaById = "spSaveProductCuotaById";
            public string spDeleteSale = "spDeleteSale";

            //REPORT PRODUCT
            public string spGetReportProductCuponByClient = "spGetReportProductCuponByClient";
            public string spReportProductCobranza = "spReportProductCobranza";
            public string spReportProductIrregularPayment = "spReportProductIrregularPayment";
            public string spReportProductQuotaPaid = "spReportProductQuotaPaid";
            public string spGetAllProductSale = "spGetAllProductSale";

        }
    }
}