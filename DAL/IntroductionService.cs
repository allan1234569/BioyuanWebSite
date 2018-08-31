using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL.Helper;

namespace DAL
{
    public class IntroductionService
    {
        public int InsertIntroduction(Introduction introduction)
        {
            string sql = "INSERT INTO Introduction(Id, CompanyIntroduction, CorporatePurpose, CorporateVision) VALUES('{0}', '{1}', '{2}', '{3}');";
            sql = string.Format(sql,
                introduction.id,
                introduction.companyIntroduction,
                introduction.corporatePurpose,
                introduction.corporateVision);

            return SQLHelper.Update(sql);
        }

        public int UpdateIntroduction(Introduction introduction)
        {
            string sql = "UPDATE Introduction SET CompanyIntroduction = '{1}', CorporatePurpose = '{2}', CorporateVision = '{3}' WHERE Id = '{0}';";
            sql = string.Format(sql,
                introduction.id,
                introduction.companyIntroduction,
                introduction.corporatePurpose,
                introduction.corporateVision);

            return SQLHelper.Update(sql);
        }

        public Introduction GetIntroductionDetail()
        {
            string sql = "SELECT * FROM Introduction";

            SqlDataReader reader = SQLHelper.GetReader(sql);

            Introduction introduction = null;

            if (reader.Read())
            {
                introduction = new Introduction();
                introduction.id = reader["Id"].ToString();
                introduction.companyIntroduction = reader["CompanyIntroduction"].ToString();
                introduction.corporatePurpose = reader["CorporatePurpose"].ToString();
                introduction.corporateVision = reader["CorporateVision"].ToString();
            }

            return introduction;
        }

        public string GetCompantIntroduction()
        {
            string sql = "SELECT CompanyIntroduction FROM Introduction";

            return (string)SQLHelper.GetSingleResult(sql);
        }


        public string GetCorporatePurpose()
        {
            string sql = "SELECT CorporatePurpose FROM Introduction";

            return (string)SQLHelper.GetSingleResult(sql);
        }

        public string GetCorporateVision()
        {
            string sql = "SELECT CorporateVision FROM Introduction";

            return (string)SQLHelper.GetSingleResult(sql);
        }
    }
}
