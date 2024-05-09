using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Wlniao;
using Wlniao.XServer;
using SqlSugar;

namespace Models
{
    /// <summary>
    /// 文章内容
    /// </summary>
    [SugarTable(SqlContext.Perfix + "article")]
    public class Article
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        /// <summary>
        /// 系统租户
        /// </summary>
        [SugarColumn(Length = 10)]
        public string owner { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(Length = 200)]
        public string title { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string attribute { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string author { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string keywords { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        [SugarColumn(Length = 150, IsNullable = true)]
        public string pic { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
        public string content { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
        public string description { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long publishtime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long updatetime { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int click { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string sid { get; set; }
        /// <summary>
        /// 所属栏目
        /// </summary>
        public int cid { get; set; }
        /// <summary>
        /// 所属栏目递归
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string cids { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        [DefaultValue(1)]
        public int site { get; set; }
        /// <summary>
        /// 状态 -1：删除 0：待审核 1：已审核 2：隐藏中 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string tags { get; set; }
        /// <summary>
        /// 重定向链接
        /// </summary>
        [SugarColumn(Length = 250, IsNullable = true)]
        public string jumpurl { get; set; }



    
    }
}