using SqlSugar;
using System.IO;
using System;
using Wlniao;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class SqlContext : SqlSugarClient
{
    /// <summary>
    /// 
    /// </summary>
    public const string Perfix = "demo_";
    private static string conn_str = null;
    private static DbType conn_type = DbType.Sqlite;
    private static string ConnStr
    {
        get
        {
            if (!string.IsNullOrEmpty(DbConnectInfo.WLN_CONNSTR_MYSQL))
            {
                conn_type = DbType.MySql;
                conn_str = DbConnectInfo.WLN_CONNSTR_MYSQL;
            }
            else
            {
                conn_type = DbType.Sqlite;
                conn_str = DbConnectInfo.WLN_CONNSTR_SQLITE;
            }
            return conn_str;
        }
    }

    private static DbType ConnType
    {
        get
        {
            return ConnStr == null ? DbType.Sqlite : conn_type;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public SqlContext() : base(new ConnectionConfig()
    {
        DbType = ConnType,
        ConnectionString = ConnStr,
        IsAutoCloseConnection = false//手动释放  是长连接 
    })
    { }
    /// <summary>
    /// 数据库初始化
    /// </summary>
    public static void Init()
    {
        // 非微服务模式运行时才执行数据库迁移
        if (Config.GetConfigs("MicroservicesNode").ToLower() != "true")
        {
            try
            {
                using (var db = new SqlContext())
                {
                    db.CodeFirst.InitTables<Models.Article>();
                    db.CodeFirst.InitTables<Models.Category>();

                    if (!db.Queryable<Models.Category>().Any())
                    {
                        var cates = new List<Models.Category>();
                        cates.Add(new Models.Category { id = strUtil.CreateMinId(), name = "新闻动态", model = "article", ename = "news", owner = "" });
                        cates.Add(new Models.Category { id = strUtil.CreateMinId(), name = "产品列表", model = "article", ename = "products", owner = "" });
                        db.Storageable<Models.Category>(cates).ExecuteCommand();
                    }
                }
            }
            catch (Exception ex)
            {
                Wlniao.log.Error("Database migrate error:" + ex.Message);
            }
        }
    }
}