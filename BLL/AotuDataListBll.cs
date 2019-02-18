using System;
using System.Collections.Generic;
using System.Text;
using PrinterManagerProject.DAL;

namespace PrinterManagerProject.BLL
{
    public partial class AotuDataListBll
    {
        private readonly PrinterManagerProject.DAL.AotuDataListDal dal = new PrinterManagerProject.DAL.AotuDataListDal();
        public AotuDataListBll()
        { }
        /// <summary>
        /// 从本地数据库获取贴签数据
        /// </summary>
        /// <param name="usedate"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        public List<PrinterManagerProject.Model.ListAllModel> getlist(string usedate, string batch)
        {
            return dal.getlist(usedate, batch);
        }
        /// <summary>
        /// 返回单瓶药品列表（不包括溶媒）
        /// </summary>
        /// <param name="Sid">溶媒ID</param>
        /// <returns></returns>
        public List<PrinterManagerProject.Model.PrintDrugModel> getPrint_y_no(int Sid)
        {
            return dal.getPrint_y_no(Sid);
        }
        /// <summary>
        /// 返回单瓶药品列表（包括溶媒）
        /// </summary>
        /// <param name="Sid">溶媒ID</param>
        /// <returns></returns>
        public List<PrinterManagerProject.Model.PrintDrugModel> getPrint_y(int Sid)
        {
            return dal.getPrint_y(Sid);
        }
        /// <summary>
        /// 根据溶媒ID修改整瓶药品的打印状态(1)，打印时间(当前)，打印模式（默认为自动0），并写入二维码
        /// </summary>
        /// <param name="Sid">溶媒ID</param>
        /// <param name="strQRcode">二维码</param>
        /// <returns></returns>
        public bool update_status(int Sid, string strQRcode)
        {
            return dal.update_status(Sid, strQRcode);
        }
    }
}
