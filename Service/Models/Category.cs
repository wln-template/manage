using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Wlniao;
using Wlniao.XServer;
using SqlSugar;
using System.Collections;

namespace Models
{
    /// <summary>
    /// 栏目
    /// </summary>
    [SugarTable(SqlContext.Perfix + "category")]
    public class Category
    {
        /// <summary>
        /// 栏目ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, Length = 15)]
        public string id { get; set; }
        /// <summary>
        /// 系统租户
        /// </summary>
        [SugarColumn(Length = 10)]
        public string owner { get; set; }
        /// <summary>
        /// 上级栏目
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        [DefaultValue(1)]
        public int site { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string pic { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(Length = 100)]
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 30)]
        public string ename { get; set; }
        /// <summary>
        /// 栏目类型
        /// </summary>
        [SugarColumn(Length = 30)]
        public string model { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString, IsNullable = true)]
        public string content { get; set; }
        /// <summary>
        /// 状态 0：隐藏 1：正常 2：停用
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string shortname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string seotitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string description { get; set; }
        /// <summary>
        /// 列表页模板
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string template_list { get; set; }
        /// <summary>
        /// 内容页模板
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string template_show { get; set; }
        /// <summary>
        /// 重定向（外部链接地址）
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string redirect { get; set; }
        /// <summary>
        /// 导航栏显示 1：是 0：否
        /// </summary>
        public int navigation { get; set; }



    }
}