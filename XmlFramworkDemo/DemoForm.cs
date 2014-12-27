/* 
 类：DemoForm
 描述：XmlFramwork增、删、改、查演示
 编 码 人：韩兆新 日期：2014年12月21日

 修改记录：

*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XmlFramwork;
using XmlFramworkDemo.Entity;
using XmlFramworkDemo.Urility;

namespace XmlFramworkDemo
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// DemoForm窗口——加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DemoForm_Load(object sender, EventArgs e)
        {
            BindGvUserInfo();
        }
        /// <summary>
        /// 添加、修改按钮——单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("请输入姓名！");
                return;
            }
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("请输入年龄！");
                return;
            }
            else if(!ValidateHelper.IsValidUintFormat(txtAge.Text.Trim()))
            {
                MessageBox.Show("年龄不是合法的格式！");
                return;
            }
            if (btnOperation.Text.Equals("添加"))
            {
                UserInfo userInfo = new UserInfo();
                userInfo.Age = uint.Parse(txtAge.Text.Trim());
                userInfo.Name = txtName.Text;
                if (!XmlEntityProcess<UserInfo>.Insert(userInfo))
                {
                    MessageBox.Show("插入失败：" + XmlEntityProcess<UserInfo>.GetLastErrMsg());
                }
            }
            else
            {
                UserInfo userInfo = btnOperation.Tag as UserInfo;
                userInfo.Name = txtName.Text;
                userInfo.Age = uint.Parse(txtAge.Text.Trim());
                if (!XmlEntityProcess<UserInfo>.Update(userInfo))
                {
                    MessageBox.Show("更新失败：" + XmlEntityProcess<UserInfo>.GetLastErrMsg());
                }
            }
            btnOperation.Text = "添加";
            BindGvUserInfo();
        }
        /// <summary>
        /// 编辑菜单——单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guid id =Guid.Parse(gvUserInfo.SelectedRows[0].Cells["ID"].Value.ToString());
            UserInfo userInfo = XmlEntityProcess<UserInfo>.GetById(id);
            txtName.Text = userInfo.Name;
            txtAge.Text = userInfo.Age.ToString();
            btnOperation.Tag = userInfo;
            btnOperation.Text = "修改";
        }
        /// <summary>
        /// 删除菜单——单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guid id = Guid.Parse(gvUserInfo.SelectedRows[0].Cells["ID"].Value.ToString());
            XmlEntityProcess<UserInfo>.DeleteById(id);
            BindGvUserInfo();
        }
        /// <summary>
        /// 查询按钮——单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guid id =Guid.Parse(gvUserInfo.SelectedRows[0].Cells["ID"].Value.ToString());
            UserInfo userInfo = XmlEntityProcess<UserInfo>.GetById(id);
            txtName.Text = userInfo.Name;
            txtAge.Text = userInfo.Age.ToString();
        }
        
        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUserInfo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (gvUserInfo.Rows[e.RowIndex].Selected == false)
                    {
                        gvUserInfo.ClearSelection();
                        gvUserInfo.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (gvUserInfo.SelectedRows.Count == 1)
                    {
                        gvUserInfo.CurrentCell = gvUserInfo.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        /// <summary>
        /// 绑定GvUserInfo
        /// </summary>
        private void BindGvUserInfo()
        {
            List<UserInfo> userInfoList = XmlEntityProcess<UserInfo>.GetAll();
            if (null != userInfoList)
            {
                gvUserInfo.DataSource = userInfoList;
            }
            else
            {
                MessageBox.Show("获取数据失败：" + XmlEntityProcess<UserInfo>.GetLastErrMsg());
            }
        }

    }
}
