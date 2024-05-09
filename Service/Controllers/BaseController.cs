namespace Controllers
{
    /// <summary>
    /// 基础控制器：对外开放服务
    /// </summary>
    public class ServiceController : Wlniao.XCenter.XAppController
    {
        /// <summary>
        /// 路由前缀
        /// </summary>
        internal const string RoutePrefix = "service/";
        /// <summary>
        /// 
        /// </summary>
        internal SqlContext db = new SqlContext();
    }

    /// <summary>
    /// 基础控制器：管理面板系统
    /// </summary>
    public class ControlController : Wlniao.XCenter.EmiController
    {
        /// <summary>
        /// 路由前缀
        /// </summary>
        internal const string RoutePrefix = "control/";
        /// <summary>
        /// 
        /// </summary>
        internal SqlContext db = new SqlContext();
    }

}