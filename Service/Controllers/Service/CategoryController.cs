using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using Wlniao;
using Wlniao.Swagger;

namespace Controllers.Control
{
    /// <summary>
    /// 内容栏目数据
    /// </summary>
    [ApiController, ApiGroup("Service")]
    [Route(RoutePrefix + "[controller]/[action]")]
    public class CategoryController : ServiceController
    {
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<ApiResult<List<ServiceCategoryListOut>>>(0)]
        [WlniaoQueryParameter(Name = "offset", Description = "分页查询偏移量", Required = false)]
        [WlniaoQueryParameter(Name = "length", Description = "输出结果长度，默认：10", Required = false)]
        public IActionResult list()
        {
            var obj = InputDeserialize();
            var skip = obj.GetInt32("offset");
            var size = obj.GetInt32("length", 10);
            var result = new ApiResult<List<ServiceCategoryListOut>>
            {
                code = "-1",
                tips = true,
                data = new List<ServiceCategoryListOut>()
            };
            try
            {
                var query = Expressionable.Create<Models.Category>();
                var rows = db.Queryable<Models.Category>().Where(query.ToExpression()).OrderBy(o => o.sort).Skip(skip).Take(size).ToList();
                foreach (var row in rows)
                {
                    result.data.Add(new ServiceCategoryListOut(row.id, row.name, row.ename, row.model, row.status, row.sort, false));
                }
                result.code = "0";
                result.message = "查询完成，数据已返回";
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return OutputSerialize(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="ename"></param>
        /// <param name="model"></param>
        /// <param name="status"></param>
        /// <param name="sort"></param>
        /// <param name="hasChildren"></param>
        public record ServiceCategoryListOut(string id, string name, string ename, string model, int status, int sort, bool hasChildren);


    }
}