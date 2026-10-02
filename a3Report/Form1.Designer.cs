namespace a3Report
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lstProduct = new System.Windows.Forms.ListBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.lstLine = new System.Windows.Forms.ListBox();
            this.chartPareto = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPareto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(30, 25);
            this.lblProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(86, 25);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product";
            // 
            // lstProduct
            // 
            this.lstProduct.FormattingEnabled = true;
            this.lstProduct.ItemHeight = 25;
            this.lstProduct.Location = new System.Drawing.Point(30, 62);
            this.lstProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstProduct.Name = "lstProduct";
            this.lstProduct.Size = new System.Drawing.Size(298, 104);
            this.lstProduct.TabIndex = 1;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(375, 25);
            this.lblLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(53, 25);
            this.lblLine.TabIndex = 2;
            this.lblLine.Text = "Line";
            // 
            // lstLine
            // 
            this.lstLine.FormattingEnabled = true;
            this.lstLine.ItemHeight = 25;
            this.lstLine.Location = new System.Drawing.Point(375, 62);
            this.lstLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstLine.Name = "lstLine";
            this.lstLine.Size = new System.Drawing.Size(298, 104);
            this.lstLine.TabIndex = 3;
            // 
            // chartPareto
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPareto.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPareto.Legends.Add(legend1);
            this.chartPareto.Location = new System.Drawing.Point(35, 216);
            this.chartPareto.Name = "chartPareto";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPareto.Series.Add(series1);
            this.chartPareto.Size = new System.Drawing.Size(2100, 833);
            this.chartPareto.TabIndex = 4;
            this.chartPareto.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2210, 1087);
            this.Controls.Add(this.chartPareto);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lstProduct);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lstLine);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Pareto Chart OLAP Report";
            ((System.ComponentModel.ISupportInitialize)(this.chartPareto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPareto;
        public System.Windows.Forms.ListBox lstProduct;
        public System.Windows.Forms.ListBox lstLine;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblLine;
    }
}

