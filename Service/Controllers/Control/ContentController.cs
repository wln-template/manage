using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;
using Wlniao;
using Wlniao.Swagger;

namespace Controllers.Control
{
    /// <summary>
    /// 控制台：内容管理服务
    /// </summary>
    [ApiController]
    [Route(RoutePrefix + "[controller]/[action]")]
    public class ContentController : ControlController
    {
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthHeader]
        public IActionResult nogroup()
        {
            return CheckSession((xsession, ctx) =>
            {
                var obj = InputDeserialize();
                var result = new ApiResult<Pager<ManageCategoryListOut>>
                {
                    code = "-1",
                    tips = true,
                    data = new Pager<ManageCategoryListOut> { page = obj.GetInt32("page"), size = obj.GetInt32("size") }
                };
                return OutputSerialize(result);
            });
        }
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthHeader, ApiGroup("Tester")]
        public IActionResult tester()
        {
            return CheckSession((xsession, ctx) =>
            {
                var obj = InputDeserialize();
                var result = new ApiResult<Pager<ManageCategoryListOut>>
                {
                    code = "-1",
                    tips = true,
                    data = new Pager<ManageCategoryListOut> { page = obj.GetInt32("page"), size = obj.GetInt32("size") }
                };
                return OutputSerialize(result);
            });
        }


        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, AuthHeader, ApiGroup("Manager")]
        public IActionResult category_list()
        {
            return CheckSession((xsession, ctx) =>
            {
                var obj = InputDeserialize();
                var key = obj.GetString("key");
                var pid = obj.GetInt32("pid");
                var site = obj.GetInt32("site");
                var status = obj.GetInt32("status");

                var result = new ApiResult<Pager<ManageCategoryListOut>>
                {
                    code = "-1",
                    tips = true,
                    data = new Pager<ManageCategoryListOut> { page = obj.GetInt32("page"), size = obj.GetInt32("size") }
                };
                try
                {
                    var query = Expressionable.Create<Models.Category>().And(o => o.site == site && o.status >= 0 && o.owner == ctx.owner);
                    if (string.IsNullOrEmpty(key))
                    {
                        query = query.And(o => o.pid == pid);
                    }
                    else
                    {
                        query = query.And(o => o.name.Contains(key) || o.ename.Contains(key));
                    }
                    if (status > 0)
                    {
                        query = query.And(o => o.status == status);
                    }
                    var total = 0;
                    var rows = db.Queryable<Models.Category>().Where(query.ToExpression()).OrderBy(o => o.sort).ToPageList(result.data.page, result.data.size, ref total);
                    result.data.total = total;
                    result.data.rows = new List<ManageCategoryListOut>();
                    foreach (var row in rows)
                    {
                        result.data.rows.Add(new ManageCategoryListOut(row.id, row.name, row.ename, row.model, row.status, row.sort, string.IsNullOrEmpty(key)));
                    }
                }
                catch (Exception ex)
                {
                    result.message = ex.Message;
                }
                return OutputSerialize(result);
            });
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
        public record ManageCategoryListOut(string id, string name, string ename, string model, int status, int sort, bool hasChildren);


    }
}