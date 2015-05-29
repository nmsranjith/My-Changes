using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DotNetNuke.Modules.HESearchResults.Data;
using DotNetNuke.Modules.HESearchResults.Controls;
using Cengage.eCommerce.Lib;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.HESearchResults.Components.Modal
{

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="productresultsearchenginemodel" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    searchenginemodel class for the product result module.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class SearchEngineModel
    {
        /// <summary>
        /// product result getcomponent function
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public SqlDataReader GetComponentResults(SearchEngine searchEngine)
        {
            return DataProvider.Instance().GetComponentResults(searchEngine);
        }

        /// <summary>
        /// product result getfacetattributes function
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public List<Facets> GetFacetResultsAttributes(SearchEngine searchEngine) //(string division, string keyword, Dictionary<string, List<int>> facets)
        {
            //Dictionary<string, List<CengageFacetSearchResult>> Dictionary = new Dictionary<string, List<CengageFacetSearchResult>>();
            List<Facets> list = CBO.FillCollection<Facets>(DataProvider.Instance().GetFacetResultsAttributes(searchEngine));
            //foreach (var item in list.Select(optgroup => optgroup.AttributeName).Distinct().ToList())
            //{
            //KeyValuePair<string,List<Person>> optgroup = new KeyValuePair<string,List<Person>>(item.ToString(),personList.Where(opt=>opt.OptGroupID==item).ToList());
            //   Dictionary.Add(item.ToString(), list.Where(opt => opt.AttributeName == item).ToList());
            //}
            return list;
        }

        /// <summary>
        /// product result getadvancekeywordresult function
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public SqlDataReader GetAdvanceKeywordResults(SearchEngine searchEngine)
        {
            return DataProvider.Instance().GetAdvanceKeywordResults(searchEngine);
        }

        /// <summary>
        /// product result getadvanceisbnresult function
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public SqlDataReader GetAdvanceIsbnResults(SearchEngine searchEngine)
        {
            return DataProvider.Instance().GetAdvanceIsbnResults(searchEngine);
        }
        public bool CheckResultsForCorrectedWord(string word, string division, int storeSk)
        {
            return DataProvider.Instance().CheckResultsForCorrectedWord(word, division, storeSk);
        }

    }
}