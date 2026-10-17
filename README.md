<div align="center">

# 📊 YoYo Manufacturing Analytics — Business Intelligence

**A full OLAP stack that turns yo-yo factory inspection data into answers: an SSAS multidimensional cube plus a WinForms app that renders live Pareto charts of production defects.**

SQL Server Analysis Services · MDX · C# WinForms · ADOMD.NET

<br>

![SSAS](https://img.shields.io/badge/SSAS_Multidimensional_Cube-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![MDX](https://img.shields.io/badge/MDX_Queries-blue?style=flat-square)
![C#](https://img.shields.io/badge/C%23_.NET_Framework_4.8-239120?style=flat-square&logo=csharp&logoColor=white)
![WinForms Charts](https://img.shields.io/badge/WinForms_Charts-0078D4?style=flat-square&logo=.net&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white)

</div>

---

## 📌 Table of Contents

- [About](#-about)
- [How It Works](#-how-it-works)
- [The Cube](#-the-cube)
- [The Pareto Report App](#-the-pareto-report-app)
- [Project Structure](#%EF%B8%8F-project-structure)
- [Getting Started](#-getting-started)

## 👋 About

A manufacturing plant produces yo-yos across multiple production lines, and every unit goes through inspection — failures get logged with a reason and location. This project builds a **business intelligence solution** on top of that inspection data:

1. A **SQL Server Analysis Services (SSAS) multidimensional cube** models the data across product, production line, defect reason, and geography dimensions.
2. A **C# WinForms reporting app** connects to the cube over ADOMD.NET, runs MDX queries based on user selections, and displays the results as an interactive **Pareto chart** — the classic "vital few, trivial many" view of which defects matter most.

The result: instead of scrolling raw inspection rows, quality managers can see at a glance which products, lines, and failure reasons drive most of the scrap and rework.

## 🔄 How It Works

```text
┌──────────────────────────┐         ┌───────────────────────────────┐
│  SQL Server (YoYo DB)    │         │  SSAS Cube (YoYoDB)           │
│  raw inspection records  │  ────▶  │  Yo Yo Product · Line ·       │
│  product · line · reason │  ETL    │  Reason · World Area dims     │
│  · state · counts        │         │  measure: [Yo Yo Data Count]  │
└──────────────────────────┘         └──────────────┬────────────────┘
                                                    │ MDX over ADOMD.NET
                                                    ▼
                                    ┌───────────────────────────────┐
                                    │  a3Report (WinForms)          │
                                    │  user picks dimensions →      │
                                    │  MDX query → Pareto chart     │
                                    │  (defect reasons by volume)   │
                                    └───────────────────────────────┘
```

## 🧊 The Cube

The `YoYoDB` cube (SSAS multidimensional, Visual StudioAnalysis Services project) is organized around one core measure — **Yo Yo Data Count** — sliced by five dimensions:

| Dimension | What it answers |
| --- | --- |
| **Yo Yo Product** | Which products fail most? (by description) |
| **Line Table** | Which production lines produce the most defects? |
| **Reason Table** | What failure reasons dominate? (the Pareto view) |
| **State Table** | Where geographically are the problems? |
| **Word Area Table** | Roll-up from state to region level |

## 📈 The Pareto Report App

The `a3Report` WinForms app is the front end:

- Connects to the deployed cube with `AdomdConnection`
- Lets the user choose an analysis angle (product, line, or reason)
- Fires the matching MDX query, e.g.

```sql
SELECT [Measures].[Yo Yo Data Count] ON COLUMNS,
       [Reason Table].[Reason Name].[Reason Name].MEMBERS ON ROWS
```

- Renders the sorted bars + cumulative line in a `System.Windows.Forms.DataVisualization` chart — the Pareto curve that shows which few reasons cause most defects
- Chart refreshes instantly on every selection change

## 🗂️ Project Structure

```text
YoYo-Manufacturing-Analytics/
├── Assisgment3/               🧊 SSAS Analysis Services project
│   ├── Assisgment3.sln        Visual Studio solution
│   ├── YoYoDB.cube            The multidimensional cube
│   ├── YoYo.ds                Data source view
│   ├── YoYo Product.dim       Product dimension
│   ├── Line Table.dim         Production line dimension
│   ├── Reason Table.dim       Defect reason dimension
│   ├── State Table.dim        State dimension
│   └── Word Area Table.dim    Region (world area) dimension
└── a3Report/                  📈 Pareto chart reporting app
    ├── Form1.cs               MDX queries + chart logic
    ├── Program.cs             Entry point
    └── packages/              ADOMD.NET client libraries
```

## 🚀 Getting Started

**Prerequisites:** Visual Studio with SQL Server Data Tools, SQL Server Analysis Services (multidimensional), and SQL Server with the YoYo inspection database restored.

1. Open `Assisgment3/Assisgment3.sln` in Visual Studio
2. Set the data source connection in `YoYo.ds`, then **Deploy** the cube to your SSAS instance
3. Process the cube
4. Open `a3Report/a3Report.sln`, point the connection string in `Form1.cs` at your SSAS instance
5. Run — pick a dimension and watch the Pareto chart build

## 👥 Credits

Built as a business intelligence / data warehousing course project.

---

<div align="center">

⭐ **Star this repo if you find it useful** ⭐

</div>
