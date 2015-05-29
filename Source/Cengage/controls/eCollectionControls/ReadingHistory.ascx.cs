using System;
using System.Web.UI.WebControls;
using System.Data;
namespace DotNetNuke.UI.eCollectionControls
{
    public partial class ReadingHistory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillReadingHistory();
        }
        protected void IndependentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView ddl = (GridView)e.Row.Cells[4].FindControl("VideoGridView");
                DataTable dt4 = new DataTable();
                DataRow dr7;

                dt4.Columns.Add("PageCount", typeof(string));
                dr7 = dt4.NewRow();
                dr7["PageCount"] = "Page One";
                dt4.Rows.Add(dr7);
                DataRow dr8;
                dr8 = dt4.NewRow();
                dr8["PageCount"] = "Page Two";
                dt4.Rows.Add(dr8);
                ddl.DataSource = dt4.DefaultView;
                ddl.DataBind();
            }

        }

        public void FillReadingHistory()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("Student", typeof(string));
            dt.Columns.Add("BookName", typeof(string));
            dt.Columns.Add("DateTime", typeof(string));
            dt.Columns.Add("VocabLogCount", typeof(string));
            dt.Columns.Add("BookOpenTime", typeof(string));

            dr = dt.NewRow();

            dr["Student"] = "Venkatesh";
            dr["BookName"] = "Name of the Book";
            dr["DateTime"] = "2.30 Monday the 6th";
            dr["VocabLogCount"] = "5";
            dr["BookOpenTime"] = "5";
            dt.Rows.Add(dr);

            TodaysGuidedRecordings.DataSource = dt.DefaultView;
            TodaysGuidedRecordings.DataBind();
            TodaysIndependentRecordings.DataSource = dt.DefaultView;
            TodaysIndependentRecordings.DataBind();


            YesterDayGuidedRecordings.DataSource = dt.DefaultView;
            YesterDayGuidedRecordings.DataBind();
            YesterDayIndependentRecordings.DataSource = dt.DefaultView;
            YesterDayIndependentRecordings.DataBind();
            RestIndependentHistory.DataSource = dt.DefaultView;
            RestIndependentHistory.DataBind();
            DataTable dt1 = new DataTable();
            DataRow dr1;
            dt1.Columns.Add("Student", typeof(string));
            dt1.Columns.Add("BookName", typeof(string));
            dt1.Columns.Add("DateTime", typeof(string));
            dt1.Columns.Add("VocabLogCount", typeof(string));
            dt1.Columns.Add("BookOpenTime", typeof(string));
            dr1 = dt1.NewRow();

            dr1["Student"] = "Venkatesh";
            dr1["BookName"] = "Name of the Book";
            dr1["DateTime"] = "3.30 Tuesday the 2nd";
            dr1["VocabLogCount"] = "7";
            dr1["BookOpenTime"] = "12";
            dt1.Rows.Add(dr1);
            RestGuidedRecordings.DataSource = dt1.DefaultView;
            RestGuidedRecordings.DataBind();

            Last7DaysIndependentRecordings.DataSource = dt1.DefaultView;
            Last7DaysIndependentRecordings.DataBind();
            //ThisMonthGridView.DataSource = dt1.DefaultView;
            //ThisMonthGridView.DataBind();

            //DataTable monthDataTable = new DataTable();
            //monthDataTable.Columns.Add("MonthName", typeof(string));
            //string[] months = {"November","October","September","August","July","June","May","April","March","Feburary","January"};
            //for(int i=0;i<11;i++)
            //{
            //    DataRow row = monthDataTable.NewRow();
            //    row["MonthName"] = months[i];
            //    monthDataTable.Rows.Add(row);
            //}
            //MonthWiseHistory.DataSource = monthDataTable;
            //MonthWiseHistory.DataBind();
        }
        protected void MonthWiseWordLog_BindInfo(object sender, RepeaterItemEventArgs e)
        {
            GridView allMonthsWordLog = (GridView)e.Item.FindControl("GuidedGridView");
            Label monthNameLabel = (Label)e.Item.FindControl("MonthLabel");

            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("Student", typeof(string));
            dt.Columns.Add("BookName", typeof(string));
            dt.Columns.Add("DateTime", typeof(string));
            dt.Columns.Add("VocabLogCount", typeof(string));
            dt.Columns.Add("BookOpenTime", typeof(string));

            dr = dt.NewRow();

            dr["Student"] = "Benjiman";
            dr["BookName"] = "Name of the Book";
            dr["DateTime"] = "2.30 Monday the 26th";
            dr["VocabLogCount"] = "5";
            dr["BookOpenTime"] = "13";
            dt.Rows.Add(dr);
            allMonthsWordLog.DataSource = dt.DefaultView;
            allMonthsWordLog.DataBind();
            if (MonthLabel.Value == monthNameLabel.Text)
                StyleApplier.Value = MonthLabel.Value;
        }

        protected void Recordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            GridView ddl = (GridView)e.Item.FindControl("VideoGridView");
            DataTable dt4 = new DataTable();
            DataRow dr7;

            dt4.Columns.Add("PageCount", typeof(string));
            dr7 = dt4.NewRow();
            dr7["PageCount"] = "Content Page 1";
            dt4.Rows.Add(dr7);
            DataRow dr8;
            dr8 = dt4.NewRow();
            dr8["PageCount"] = "Content Page 2";
            dt4.Rows.Add(dr8);
            ddl.DataSource = dt4.DefaultView;
            ddl.DataBind();

        }
    }
}