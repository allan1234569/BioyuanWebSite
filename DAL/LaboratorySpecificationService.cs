using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL.Helper;
using Models;

namespace DAL
{
    public class LaboratorySpecificationService
    {
        public int InsertLaboratorySpecification(LaboratorySpecification labSpec)
        {
            string sql = "INSERT INTO LaboratorySpecification(SpecificationId, LaboratoryQualityControlId, ProductCode, Concentration, Specification, CertificateNo) VALUES('{0}','{1}','{2}','{3}','{4}','{5}');";

            sql = string.Format(sql,
                labSpec.SpecificationId,
                labSpec.LaboratoryQualityControlId,
                labSpec.ProductCode,
                labSpec.Concentration,
                labSpec.CertificateNo);

            return SQLHelper.Update(sql);
        }

        public int InsertLaboratorySpecificationBatch(List<LaboratorySpecification> labSpecList)
        {
            string sql = "INSERT INTO LaboratorySpecification(SpecificationId, LaboratoryQualityControlId, ProductCode, Concentration, Specification, CertificateNo) VALUES";

            string valuesSql = "";

            foreach (LaboratorySpecification labSpec in labSpecList)
            {
                string subSql = "('{0}','{1}','{2}','{3}','{4}','{5}'),";
                subSql = string.Format(subSql, 
                    labSpec.SpecificationId,
                    labSpec.LaboratoryQualityControlId,
                    labSpec.ProductCode,
                    labSpec.Concentration,
                    labSpec.Specification,
                    labSpec.CertificateNo);

                valuesSql += subSql;
            }

            if (valuesSql.Contains(','))
            {
                valuesSql = valuesSql.Substring(0, valuesSql.Length - 1) + ";";
            }

            sql += valuesSql;

            return SQLHelper.Update(sql);
        }
    }
}
