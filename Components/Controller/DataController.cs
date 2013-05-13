// ***********************************************************************
// Assembly         : QSContent
// Author           : Jonathan Sheely
// Created          : 05-12-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 05-12-2013
// ***********************************************************************
// <copyright file="DataController.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Host;
using InspectorIT.QSContent.Components.Common;
using InspectorIT.QSContent.Components.Entities;

namespace InspectorIT.QSContent.Components.Controller
{
    /// <summary>
    /// Class DataController
    /// </summary>
    public class DataController
    {
        /// <summary>
        /// The data prefix
        /// </summary>
        private const string DataPrefix = "InspectorIT_QSContent_";

        /// <summary>
        /// Adds the check.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="querystring">The querystring.</param>
        /// <param name="hideMatch">if set to <c>true</c> [hide match].</param>
        /// <param name="checkModuleId">The check module id.</param>
        /// <param name="userId">The user id.</param>
        public static void AddCheck(int moduleId, string querystring,bool hideMatch, int checkModuleId, int userId)
        {
            //CheckInfo check = new CheckInfo();
            //check.Querystring = querystring;
            //check.ModuleID = moduleId;
            //check.CreatedByUserID = userId;

            DataProvider.Instance().ExecuteNonQuery(DataPrefix + "AddCheck",moduleId, querystring,hideMatch, checkModuleId, userId);
            ClearCache(moduleId);
        }

        /// <summary>
        /// Gets the checks.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns>List{CheckInfo}.</returns>
        public static List<CheckInfo> GetChecks(int moduleId)
        {

            var cache = DataCache.GetCache(Constants.ModuleCacheKey + moduleId.ToString()) as List<CheckInfo>;
            if (cache == null)
            {
                var timeOut = 20 * Convert.ToInt32(Host.PerformanceSetting);
                cache = GetChecks(moduleId, true);
                if (timeOut > 0 & cache != null)
                {
                    DataCache.SetCache(Constants.ModuleCacheKey + moduleId.ToString(), cache, TimeSpan.FromMinutes(timeOut));
                }
            }
            return cache;

        }

        /// <summary>
        /// Gets the checks.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="ignoreCache">if set to <c>true</c> [ignore cache].</param>
        /// <returns>List{CheckInfo}.</returns>
        public static List<CheckInfo> GetChecks(int moduleId, bool ignoreCache)
        {
            if (ignoreCache)
            {
                return CBO.FillCollection<CheckInfo>(DataProvider.Instance().ExecuteReader(DataPrefix + "GetChecks", moduleId));
            }
            return GetChecks(moduleId);
        }

        /// <summary>
        /// Deletes the check.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="moduleId">The module id.</param>
        public static void DeleteCheck(int id, int moduleId)
        {
            DataProvider.Instance().ExecuteNonQuery(DataPrefix + "DeleteCheck", id);
            ClearCache(moduleId);
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        private static void ClearCache(int moduleId)
        {
            var cache = DataCache.GetCache(Constants.ModuleCacheKey + moduleId.ToString()) as List<CheckInfo>;
            if (cache == null)
            {
                DataCache.RemoveCache(Constants.ModuleCacheKey + moduleId.ToString());
            }
        }

    }
}