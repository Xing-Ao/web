﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Collections.Specialized;
using XingAo.Core.Data;
using XingAo.Networks.CMS.Model.Enums;

namespace XingAo.Networks.CMS.Web.Manager.Wechat.Activities.BigWheel
{
    public partial class Main : Common.BaseListPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var mywhere = QueryBuilder.Create<Model.BigWheel>().Like(c => c.Title, keyString);
                mywhere = mywhere.Equals(c => c.WGuid, "");
                int r = 0;
                var data = new UnitOfWork().LoadWhereLambda(mywhere.Expression, p => p.Where(c => c.IsDelete != 1).OrderByDescending(c => c.ID), PageNum, PageSize, out r);
                TotalCount = r;//绑定前必须给总记录数赋值

                var datas = data.Select(c => new { c.ID, c.Keys, c.Title, CanUse = WeiWebEnum.StateSwitch(c.CanUse), c.StartTime, c.EndTime, c.BGuid ,c.OrderID}).ToList();
                Rep_List.DataSource = datas;
                Rep_List.DataBind();       

            }
        }
    }
}