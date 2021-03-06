using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cryptoexch
{
    public class GridViewExtended : GridView
    {
        #region Public Properties

        [Category("Behavior")]
        [Themeable(true)]
        [Bindable(BindableSupport.No)]
        public bool ShowFooterWhenEmpty
        {
            get
            {
                if (this.ViewState["ShowFooterWhenEmpty"] == null)
                {
                    this.ViewState["ShowFooterWhenEmpty"] = false;
                }

                return (bool)this.ViewState["ShowFooterWhenEmpty"];
            }
            set
            {
                this.ViewState["ShowFooterWhenEmpty"] = value;
            }
        }

        #endregion Public Properties

        private GridViewRow _footerRow2;

        public override GridViewRow FooterRow
        {
            get
            {
                GridViewRow f = base.FooterRow;
                if (f != null)
                    return f;
                else
                    return _footerRow2;
            }
        }

        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            int rows = base.CreateChildControls(dataSource, dataBinding);

            //  no data rows created, create empty table if enabled
            if (rows == 0 && (this.ShowFooterWhenEmpty))
            {
                //  create the table
                Table table = this.CreateChildTable();

                DataControlField[] fields;
                if (this.AutoGenerateColumns)
                {
                    PagedDataSource source = new PagedDataSource();
                    source.DataSource = dataSource;

                    System.Collections.ICollection autoGeneratedColumns = this.CreateColumns(source, true);
                    fields = new DataControlField[autoGeneratedColumns.Count];
                    autoGeneratedColumns.CopyTo(fields, 0);
                }
                else
                {
                    fields = new DataControlField[this.Columns.Count];
                    this.Columns.CopyTo(fields, 0);
                }

                if (this.ShowHeaderWhenEmpty)
                {
                    //  create a new header row
                    GridViewRow headerRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
                    this.InitializeRow(headerRow, fields);

                    //  add the header row to the table
                    table.Rows.Add(headerRow);
                }

                //  create the empty row
                GridViewRow emptyRow = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.ColumnSpan = fields.Length;
                cell.Width = Unit.Percentage(100);

                //  respect the precedence order if both EmptyDataTemplate
                //  and EmptyDataText are both supplied ...
                if (this.EmptyDataTemplate != null)
                {
                    this.EmptyDataTemplate.InstantiateIn(cell);
                }
                else if (!string.IsNullOrEmpty(this.EmptyDataText))
                {
                    cell.Controls.Add(new LiteralControl(EmptyDataText));
                }

                emptyRow.Cells.Add(cell);
                table.Rows.Add(emptyRow);

                if (this.ShowFooterWhenEmpty)
                {
                    //  create footer row
                    _footerRow2 = base.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
                    this.InitializeRow(_footerRow2, fields);

                    //  add the footer to the table
                    table.Rows.Add(_footerRow2);
                }

                this.Controls.Clear();
                this.Controls.Add(table);
            }

            return rows;
        }
    }
}