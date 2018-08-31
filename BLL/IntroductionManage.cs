using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL
{
    public class IntroductionManage
    {
        public int InsertIntroduction(Introduction introduction)
        {
            return new IntroductionService().InsertIntroduction(introduction);
        }

        public int UpdateIntroduction(Introduction introduction)
        {
            return new IntroductionService().UpdateIntroduction(introduction);
        }

        public Introduction GetIntroductionDetail()
        {
            return new IntroductionService().GetIntroductionDetail();
        }

        public string GetCompantIntroduction()
        {
            string sql = "SELECT CompanyIntroduction FROM Introduction";

            return new IntroductionService().GetCompantIntroduction();
        }


        public string GetCorporatePurpose()
        {
            string sql = "SELECT CorporatePurpose FROM Introduction";

            return new IntroductionService().GetCorporatePurpose();
        }

        public string GetCorporateVision()
        {
            string sql = "SELECT CorporateVision FROM Introduction";

            return new IntroductionService().GetCorporateVision();
        }
    }
}
