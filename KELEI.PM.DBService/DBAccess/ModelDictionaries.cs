using System;
using System.Collections.Generic;

namespace KELEI.PM.DBService.DBAccess
{
    public static class ModelDictionaries
    {
        /// <summary>
        /// 加载到内存中的业务实体的结构，
        /// Key:业务实体ID，
        /// value：业务实体结构
        /// </summary>
        private static Dictionary<string, BusinessModel> modelDiction = new Dictionary<string, BusinessModel>();


        public static Dictionary<string, BusinessModel> GetDicModel()
        {
            return modelDiction;
        }

        public static BusinessModel GetDicModel(string dicID)
        {
            //判断是否存在
            if (modelDiction.ContainsKey(dicID))
            {
                return modelDiction[dicID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 修改字典数据
        /// </summary>
        public static void UpdateDicModel(string dicID, BusinessModel dicModel)
        {
            //判断是否存在
            if (modelDiction.ContainsKey(dicID))
            {
                modelDiction[dicID] = dicModel;
            }
            else
            {
                //不存在，新增
                modelDiction.Add(dicID, dicModel);
            }
        }

        public static void DeleteDicModel(string dicID)
        {
            //判断是否存在
            if (modelDiction.ContainsKey(dicID))
            {
                modelDiction.Remove(dicID);
            }
            else
            {
                //不存在，不做处理
            }
        }

        public static void DeleteDicAll()
        {
            if (modelDiction != null)
            {
                modelDiction.Clear();
                modelDiction = null;
            }
        }
        //========================================================================================================//
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, AnalysisModel> analysisDiction = new Dictionary<string, AnalysisModel>();
        public static Dictionary<string, AnalysisModel> GetDicModelAnalysis()
        {
            return analysisDiction;
        }

        public static AnalysisModel GetDicModelAnalysis(string dicID)
        {
            //判断是否存在
            if (analysisDiction.ContainsKey(dicID))
            {
                return analysisDiction[dicID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 修改字典数据
        /// </summary>
        public static void UpdateDicModelAnalysis(string dicID, AnalysisModel dicModel)
        {
            //判断是否存在
            if (analysisDiction.ContainsKey(dicID))
            {
                analysisDiction[dicID] = dicModel;
            }
            else
            {
                //不存在，新增
                analysisDiction.Add(dicID, dicModel);
            }
        }

        public static void DeleteDicModelAnalysis(string dicID)
        {
            //判断是否存在
            if (analysisDiction.ContainsKey(dicID))
            {
                analysisDiction.Remove(dicID);
            }
            else
            {
                //不存在，不做处理
            }
        }

        public static void DeleteDicModelAnalysisAll()
        {
            if (analysisDiction != null)
            {
                analysisDiction.Clear();
                analysisDiction = null;
            }
        }
        //
        public static void DeleteDic(string dicID)
        {
            DeleteDicModelAnalysis(dicID);
            DeleteDicModel(dicID);
        }

        public static Tuple<BusinessModel, AnalysisModel> GetDic(string dicID)
        {
            //判断是否存在
            var bm = GetDicModel(dicID);
            var am = GetDicModelAnalysis(dicID);
            return new Tuple<BusinessModel, AnalysisModel>(bm, am);
        }
    }
}
