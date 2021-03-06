USE [master]
GO
/****** Object:  Database [AutoSortingDB]    Script Date: 02/25/2019 18:19:21 ******/
CREATE DATABASE [AutoSortingDB] ON  PRIMARY 
( NAME = N'AutoSortingDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\AutoSortingDB.mdf' , SIZE = 3328KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AutoSortingDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\AutoSortingDB_log.LDF' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AutoSortingDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoSortingDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AutoSortingDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [AutoSortingDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [AutoSortingDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [AutoSortingDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [AutoSortingDB] SET ARITHABORT OFF
GO
ALTER DATABASE [AutoSortingDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [AutoSortingDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [AutoSortingDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [AutoSortingDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [AutoSortingDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [AutoSortingDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [AutoSortingDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [AutoSortingDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [AutoSortingDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [AutoSortingDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [AutoSortingDB] SET  ENABLE_BROKER
GO
ALTER DATABASE [AutoSortingDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [AutoSortingDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [AutoSortingDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [AutoSortingDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [AutoSortingDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [AutoSortingDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [AutoSortingDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [AutoSortingDB] SET  READ_WRITE
GO
ALTER DATABASE [AutoSortingDB] SET RECOVERY FULL
GO
ALTER DATABASE [AutoSortingDB] SET  MULTI_USER
GO
ALTER DATABASE [AutoSortingDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [AutoSortingDB] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'AutoSortingDB', N'ON'
GO
USE [AutoSortingDB]
GO
/****** Object:  Table [dbo].[tUser]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[JobNum] [nvarchar](100) NULL,
 CONSTRAINT [PK_TUSER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRole]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_TROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tOrderSummary]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tOrderSummary](
	[Id] [uniqueidentifier] NOT NULL,
	[use_date] [varchar](10) NULL,
	[batch] [varchar](20) NULL,
	[batchName] [nvarchar](20) NULL,
	[DepartmentId] [int] NOT NULL,
	[ShortDepartmentName] [nvarchar](50) NULL,
	[CollectUserId] [int] NULL,
	[CollectUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[TotalCount] [int] NOT NULL,
	[AutoCount] [int] NOT NULL,
	[ManualCount] [int] NOT NULL,
	[HuaLiaoCount] [int] NOT NULL,
	[YingYangYeCount] [int] NOT NULL,
	[PackageCount] [int] NOT NULL,
	[AvoidLightCount] [int] NOT NULL,
	[OtherCount] [int] NOT NULL,
 CONSTRAINT [PK_TORDERSUMMARY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tOrderBak]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tOrderBak](
	[Id] [uniqueidentifier] NOT NULL,
	[group_num] [varchar](20) NULL,
	[use_date] [varchar](10) NULL,
	[use_time] [varchar](10) NULL,
	[batch] [varchar](20) NULL,
	[batchName] [varchar](50) NULL,
	[drug_id] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[special_medicationtip] [varchar](100) NULL,
	[drug_spec] [varchar](50) NULL,
	[patient_id] [varchar](20) NULL,
	[patient_name] [varchar](50) NULL,
	[departmengt_name] [varchar](50) NULL,
	[department_code] [varchar](50) NULL,
	[zone] [int] NULL,
	[bed_number] [varchar](20) NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[order_status] [varchar](4) NULL,
	[DepartmentId] [int] NOT NULL,
	[ShortDepartmentName] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[BasketNum] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[BarCode] [varchar](50) NULL,
	[CollectModel] [nvarchar](50) NULL,
	[CollectState] [nvarchar](50) NULL,
	[Spec] [varchar](10) NULL,
	[CollectTime] [datetime] NULL,
	[CollectUserId] [int] NULL,
	[CollectUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[CheckTime] [datetime] NULL,
	[IsAvoidLight] [bit] NOT NULL,
	[IsPayFee] [bit] NOT NULL,
	[IsFinishChecked] [bit] NOT NULL,
	[IsStop] [bit] NOT NULL,
	[order_sub_no] [varchar](20) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[IsPackage] [bit] NOT NULL,
 CONSTRAINT [PK_tOrderBak] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tOrder]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tOrder](
	[Id] [uniqueidentifier] NOT NULL,
	[group_num] [varchar](20) NULL,
	[use_date] [varchar](10) NULL,
	[use_time] [varchar](10) NULL,
	[batch] [varchar](20) NULL,
	[batchName] [varchar](50) NULL,
	[drug_id] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[special_medicationtip] [varchar](100) NULL,
	[drug_spec] [varchar](50) NULL,
	[patient_id] [varchar](20) NULL,
	[patient_name] [varchar](50) NULL,
	[departmengt_name] [varchar](50) NULL,
	[department_code] [varchar](50) NULL,
	[zone] [int] NULL,
	[bed_number] [varchar](20) NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[order_status] [varchar](4) NULL,
	[DepartmentId] [int] NOT NULL,
	[ShortDepartmentName] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[BasketNum] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[BarCode] [varchar](50) NULL,
	[CollectModel] [nvarchar](50) NULL,
	[CollectState] [nvarchar](50) NULL,
	[Spec] [varchar](10) NULL,
	[CollectTime] [datetime] NULL,
	[CollectUserId] [int] NULL,
	[CollectUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[CheckTime] [datetime] NULL,
	[IsAvoidLight] [bit] NOT NULL,
	[IsPayFee] [bit] NOT NULL,
	[IsFinishChecked] [bit] NOT NULL,
	[IsStop] [bit] NOT NULL,
	[order_sub_no] [varchar](20) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[IsPackage] [bit] NOT NULL,
 CONSTRAINT [PK_TORDER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tDrug_for_View]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tDrug_for_View](
	[drug_code] [varchar](50) NULL,
	[drug_name] [varchar](100) NULL,
	[drug_spec] [varchar](50) NULL,
	[drug_units] [varchar](50) NULL,
	[drug_use_spec] [varchar](22) NULL,
	[drug_use_units] [varchar](50) NULL,
	[drug_form] [varchar](100) NULL,
	[input_code] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tDept_for_View]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tDept_for_View](
	[Code] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[ShortCut] [varchar](30) NULL,
	[Isuse] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tDepartment]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDepartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShortName] [nvarchar](50) NULL,
	[PivalId] [nvarchar](50) NOT NULL,
	[PivasName] [nvarchar](50) NOT NULL,
	[BasketNum] [int] NOT NULL,
	[Zone] [nvarchar](200) NULL,
 CONSTRAINT [PK_TDEPARTMENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tDepartment', @level2type=N'COLUMN',@level2name=N'Zone'
GO
/****** Object:  Table [dbo].[tDeliverySummary]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tDeliverySummary](
	[Id] [uniqueidentifier] NOT NULL,
	[use_date] [varchar](10) NULL,
	[batch] [varchar](20) NULL,
	[batchName] [nvarchar](20) NULL,
	[TotalCount] [int] NOT NULL,
	[DeliveryCount] [int] NULL,
	[DiffCount] [int] NULL,
	[departmengt_name] [varchar](50) NULL,
	[department_code] [varchar](50) NULL,
	[DepartmentId] [int] NOT NULL,
	[ShortDepartmentName] [nvarchar](50) NULL,
	[DepartmentAddress] [nvarchar](50) NULL,
	[DeliveryTime] [varchar](20) NULL,
 CONSTRAINT [PK_TDELIVERYSUMMARY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tConfig]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Value] [nvarchar](200) NULL,
	[GroupCode] [nvarchar](100) NULL,
 CONSTRAINT [PK_TCONFIG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tBatch_for_View]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tBatch_for_View](
	[batch] [varchar](10) NULL,
	[batch_name] [varchar](10) NULL,
	[start_time] [varchar](10) NULL,
	[end_time] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tBatch]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tBatch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[batch] [varchar](10) NULL,
	[batch_name] [varchar](10) NULL,
	[start_time] [varchar](10) NULL,
	[end_time] [varchar](10) NULL,
 CONSTRAINT [PK_TBATCH] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tZHYBak]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tZHYBak](
	[Id] [uniqueidentifier] NOT NULL,
	[drug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[usage_id] [varchar](50) NULL,
	[use_org] [varchar](10) NULL,
	[use_count] [varchar](20) NULL,
	[durg_use_sp] [varchar](20) NULL,
	[drug_use_units] [varchar](10) NULL,
	[use_frequency] [varchar](20) NULL,
	[use_date] [varchar](10) NULL,
	[use_time] [varchar](10) NULL,
	[stop_date_time] [varchar](23) NULL,
	[start_date_time] [varchar](23) NULL,
	[order_sub_no] [varchar](20) NULL,
	[order_type] [varchar](10) NULL,
	[icatrepeat_indorm] [varchar](4) NULL,
	[new_orders] [int] NULL,
	[yebz] [int] NULL,
	[special_medicationtip] [varchar](100) NULL,
	[size_specification] [int] NULL,
	[pass_remark] [varchar](100) NULL,
	[patient_id] [varchar](20) NULL,
	[doctor_name] [varchar](30) NULL,
	[patient_name] [varchar](50) NULL,
	[batch] [varchar](20) NULL,
	[departmengt_name] [varchar](50) NULL,
	[department_code] [varchar](50) NULL,
	[zone] [int] NULL,
	[visit_id] [varchar](20) NULL,
	[group_num] [varchar](20) NULL,
	[sum_num] [varchar](20) NULL,
	[ml_speed] [int] NULL,
	[create_date] [varchar](23) NULL,
	[order_status] [varchar](4) NULL,
	[is_twice_print] [int] NULL,
	[checker] [varchar](20) NULL,
	[deliveryer] [varchar](20) NULL,
	[config_person] [varchar](20) NULL,
	[config_date] [varchar](23) NULL,
	[usage_name] [varchar](50) NULL,
	[bed_number] [varchar](20) NULL,
	[basket_number] [int] NULL,
	[sorting_status] [int] NULL,
	[sorting_model] [int] NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[barcode] [varchar](100) NULL,
	[is_print_snv] [varchar](23) NULL,
 CONSTRAINT [PK_tZHYBak] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tZHY_for_View]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tZHY_for_View](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[drug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[usage_id] [varchar](50) NULL,
	[use_org] [varchar](10) NULL,
	[use_count] [varchar](20) NULL,
	[durg_use_sp] [varchar](20) NULL,
	[drug_use_units] [varchar](10) NULL,
	[use_frequency] [varchar](20) NULL,
	[use_date] [varchar](10) NULL,
	[use_time] [varchar](10) NULL,
	[stop_date_time] [varchar](23) NULL,
	[start_date_time] [varchar](23) NULL,
	[order_sub_no] [varchar](20) NULL,
	[order_type] [varchar](10) NULL,
	[icatrepeat_indorm] [varchar](4) NULL,
	[new_orders] [int] NULL,
	[yebz] [int] NULL,
	[special_medicationtip] [varchar](100) NULL,
	[size_specification] [int] NULL,
	[pass_remark] [varchar](100) NULL,
	[patient_id] [varchar](20) NULL,
	[doctor_name] [varchar](30) NULL,
	[patient_name] [varchar](50) NULL,
	[batch] [varchar](20) NULL,
	[departmengt_name] [varchar](50) NULL,
	[department_code] [varchar](50) NULL,
	[zone] [int] NULL,
	[visit_id] [varchar](20) NULL,
	[group_num] [varchar](20) NULL,
	[sum_num] [varchar](20) NULL,
	[ml_speed] [int] NULL,
	[create_date] [varchar](23) NULL,
	[order_status] [varchar](4) NULL,
	[is_twice_print] [int] NULL,
	[checker] [varchar](20) NULL,
	[deliveryer] [varchar](20) NULL,
	[config_person] [varchar](20) NULL,
	[config_date] [varchar](23) NULL,
	[usage_name] [varchar](50) NULL,
	[bed_number] [varchar](20) NULL,
	[basket_number] [int] NULL,
	[sorting_status] [int] NULL,
	[sorting_model] [int] NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[barcode] [nvarchar](100) NULL,
	[is_print_snv] [varchar](23) NULL,
	[sex] [varchar](4) NULL,
 CONSTRAINT [PK_tZHY_for_View] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品权重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品商品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_spmc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品作用代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_class_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'usage_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'use_org'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'use_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'durg_use_sp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'drug_use_units'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用频次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'use_frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'use_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'use_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'stop_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'start_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱子序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'order_sub_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'order_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长期/临时医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'icatrepeat_indorm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'new_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'婴儿标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'yebz'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊用药提示（先用、半量、冷藏、特殊低速、避光滴注、儿童慎用、18岁以下禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'special_medicationtip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大小规格小计量药品用下划线标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'size_specification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'静配备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'pass_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'患者id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'patient_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医生姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'doctor_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'patient_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'batch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'departmengt_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'department_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'zone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次住院标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'visit_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'group_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'sum_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'低速' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'ml_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'create_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱状态（正常/停药）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'order_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否重打' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'is_twice_print'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'checker'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摆药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'deliveryer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'config_person'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'config_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'usage_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'床位编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'bed_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药篮编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'basket_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分拣状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'sorting_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分拣模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'sorting_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'electroni_signature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成品复核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'is_cpfh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否收费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'is_sf'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打包药' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'is_db'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'config_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二维码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tZHY_for_View', @level2type=N'COLUMN',@level2name=N'barcode'
GO
/****** Object:  Table [dbo].[tZHY]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tZHY](
	[Id] [uniqueidentifier] NOT NULL,
	[drug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[usage_id] [varchar](50) NULL,
	[use_org] [varchar](10) NULL,
	[use_count] [varchar](20) NULL,
	[durg_use_sp] [varchar](20) NULL,
	[drug_use_units] [varchar](10) NULL,
	[use_frequency] [varchar](20) NULL,
	[use_date] [varchar](10) NULL,
	[use_time] [varchar](10) NULL,
	[stop_date_time] [varchar](23) NULL,
	[start_date_time] [varchar](23) NULL,
	[order_sub_no] [varchar](20) NULL,
	[order_type] [varchar](10) NULL,
	[icatrepeat_indorm] [varchar](4) NULL,
	[new_orders] [int] NULL,
	[yebz] [int] NULL,
	[special_medicationtip] [varchar](100) NULL,
	[size_specification] [int] NULL,
	[pass_remark] [varchar](100) NULL,
	[patient_id] [varchar](20) NULL,
	[doctor_name] [varchar](30) NULL,
	[patient_name] [varchar](50) NULL,
	[batch] [varchar](20) NULL,
	[departmengt_name] [varchar](50) NULL,
	[department_code] [varchar](50) NULL,
	[zone] [int] NULL,
	[visit_id] [varchar](20) NULL,
	[group_num] [varchar](20) NULL,
	[sum_num] [varchar](20) NULL,
	[ml_speed] [int] NULL,
	[create_date] [varchar](23) NULL,
	[order_status] [varchar](4) NULL,
	[is_twice_print] [int] NULL,
	[checker] [varchar](20) NULL,
	[deliveryer] [varchar](20) NULL,
	[config_person] [varchar](20) NULL,
	[config_date] [varchar](23) NULL,
	[usage_name] [varchar](50) NULL,
	[bed_number] [varchar](20) NULL,
	[basket_number] [int] NULL,
	[sorting_status] [int] NULL,
	[sorting_model] [int] NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[barcode] [varchar](100) NULL,
	[is_print_snv] [varchar](23) NULL,
 CONSTRAINT [PK_tZHY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tWarningSummary]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tWarningSummary](
	[Id] [uniqueidentifier] NOT NULL,
	[use_date] [varchar](10) NULL,
	[batch] [varchar](20) NULL,
	[batchName] [nvarchar](20) NULL,
	[ExceptionType] [nvarchar](50) NOT NULL,
	[TotalCount] [int] NOT NULL,
	[CollectUserId] [int] NULL,
	[CollectUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[departmengt_name] [varchar](50) NULL,
	[DepartmentId] [int] NULL,
	[ShortDepartmentName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TWARNINGSUMMARY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tWarning]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tWarning](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NULL,
	[group_num] [varchar](20) NULL,
	[use_date] [varchar](10) NULL,
	[use_time] [varchar](10) NULL,
	[batch] [varchar](20) NULL,
	[batchName] [varchar](50) NULL,
	[order_sub_no] [varchar](20) NULL,
	[drug_id] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[patient_id] [varchar](20) NULL,
	[patient_name] [varchar](50) NULL,
	[departmengt_name] [varchar](50) NULL,
	[DepartmentId] [int] NULL,
	[ShortDepartmentName] [nvarchar](50) NULL,
	[zone] [int] NULL,
	[bed_number] [varchar](20) NULL,
	[BarCode] [varchar](50) NULL,
	[Spec] [varchar](10) NULL,
	[IsAvoidLight] [bit] NULL,
	[IsPayFee] [bit] NULL,
	[IsFinishChecked] [bit] NULL,
	[IsStop] [bit] NULL,
	[CollectTime] [datetime] NULL,
	[CollectUserId] [int] NULL,
	[CollectUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[CheckTime] [datetime] NULL,
	[CollectModel] [nvarchar](50) NULL,
	[CollectState] [nvarchar](50) NULL,
	[ExceptionType] [nvarchar](50) NOT NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[IsPackage] [bit] NOT NULL,
 CONSTRAINT [PK_TWARNING] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[v_for_ydwl_drug]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_for_ydwl_drug]
AS
SELECT   drug_code, drug_name, drug_spec, drug_units, drug_use_spec, drug_use_units, drug_form, input_code
FROM      dbo.tDrug_for_View
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tDrug_for_View"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl_drug'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl_drug'
GO
/****** Object:  View [dbo].[v_for_ydwl_dept]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_for_ydwl_dept]
AS
SELECT   Code, Name, ShortCut, Isuse
FROM      dbo.tDept_for_View
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tDept_for_View"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl_dept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl_dept'
GO
/****** Object:  View [dbo].[v_for_ydwl_batch]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_for_ydwl_batch]
AS
SELECT   dbo.tBatch_for_View.*
FROM      dbo.tBatch_for_View
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tBatch_for_View"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 196
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl_batch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl_batch'
GO
/****** Object:  View [dbo].[v_for_ydwl]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_for_ydwl]
AS
SELECT     Id, drug_id, drug_number, drug_name, drug_weight, drug_spmc, drug_class_name, drug_spec, usage_id, use_org, use_count, durg_use_sp, drug_use_units, 
                      use_frequency, CONVERT(varchar(100), GETDATE(), 23) AS use_date, use_time, stop_date_time, start_date_time, order_sub_no, order_type, icatrepeat_indorm, 
                      new_orders, yebz, special_medicationtip, size_specification, pass_remark, patient_id, doctor_name, patient_name, batch, departmengt_name, department_code, 
                      zone, visit_id, group_num + CONVERT(nvarchar(10), (Id - 1) / 2) AS group_num, sum_num, ml_speed, create_date, order_status, is_twice_print, checker, deliveryer, 
                      config_person, config_date, usage_name, bed_number, basket_number, sorting_status, sorting_model, electroni_signature, is_cpfh, is_sf, age, is_db, config_name, 
                      group_num + CONVERT(varchar(100), GETDATE(), 23) + use_time AS barcode, is_print_snv, sex
FROM         dbo.tZHY_for_View
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[39] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tZHY_for_View"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 251
            End
            DisplayFlags = 280
            TopColumn = 53
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1470
         Alias = 1020
         Table = 1455
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_for_ydwl'
GO
/****** Object:  Table [dbo].[tUserRole]    Script Date: 02/25/2019 18:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_TUSERROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[P_InsertIntotOrderSelecttZHY]    Script Date: 02/25/2019 18:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_InsertIntotOrderSelecttZHY]
	-- Add the parameters for the stored procedure here
	@use_date varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--declare @use_date varchar(20) ='2019-02-23'
INSERT INTO [dbo].[tOrder]
           ([Id]
           ,[drug_id]
           ,[drug_name]
           ,[drug_spec]
           ,[group_num]
           ,[use_date]
           ,[use_time]
           ,[batch]
           ,[batchName]
           ,[special_medicationtip]
           ,[patient_id]
           ,[patient_name]
           ,[departmengt_name]
           ,[department_code]
           ,[zone]
           ,[bed_number]
           ,[is_cpfh]
           ,[is_sf]
           ,[order_status]
           ,[DepartmentId]
           ,[ShortDepartmentName]
           ,[Status]
           ,[BasketNum]
           ,[Number]
           ,[BarCode]
           ,[CollectModel]
           ,[CollectState]
           ,[Spec]
           ,[CollectTime]
           ,[CollectUserId]
           ,[CollectUserName]
           ,[CheckUserId]
           ,[CheckUserName]
           ,[CheckTime]
           ,[IsAvoidLight]
           ,[IsPayFee]
           ,[IsFinishChecked]
           ,[IsStop]
           ,[order_sub_no]
           ,[age]
           ,[is_db]
           ,[config_name]
           ,[IsPackage])
SELECT newid() as [Id]
      ,tZHY.[drug_id]
      ,tZHY.[drug_name]
      ,tZHY.[drug_spec]
      ,tZHY.[group_num]
      ,tZHY.[use_date]
      ,tZHY.[use_time]
      ,tZHY.[batch]
      ,tBatch.batch_name as [batchName]
      ,tZHY.[special_medicationtip]
      ,tZHY.[patient_id]
      ,tZHY.[patient_name]
      ,tZHY.[departmengt_name]
      ,tZHY.[department_code]
      ,tZHY.[zone]
      ,tZHY.[bed_number]
      ,tZHY.[is_cpfh]
      ,tZHY.[is_sf]
      ,tZHY.[order_status]
      ,tDepartment.Id as [DepartmentId]
      ,tDepartment.ShortName as [ShortDepartmentName]
      ,0 as [Status]
      ,tDepartment.BasketNum [BasketNum]
      ,0 [Number]
      ,tZHY.[barcode]
      ,null as [CollectModel]
      ,'未分拣' as [CollectState]
      ,case convert(varchar(max),CHARINDEX('100ml',lower(tZHY.drug_spec+tZHY.drug_name),0))+convert(varchar(max),CHARINDEX('250ml',lower(tZHY.drug_spec+tZHY.drug_name),0))+ convert(varchar(max),CHARINDEX('500ml',lower(tZHY.drug_spec+tZHY.drug_name),0)) when '100' then '100ml' when '010' then '250ml' when '001' then '500ml' else '' end [Spec]
      ,null as [CollectTime]
      ,null as [CollectUserId]
      ,null as [CollectUserName]
      ,null as [CheckUserId]
      ,null as [CheckUserName]
      ,null as [CheckTime]
      ,case CHARINDEX('避光',tZHY.special_medicationtip) when 0 then  0 else 1 end [IsAvoidLight]
      ,case tZHY.is_sf when '是' then  1 else 0 end as [IsPayFee]
      ,case tZHY.order_status when '停药' then  1 else 0 end [IsFinishChecked]
      ,case tZHY.is_cpfh when '是' then  1 else 0 end as [IsStop]
      ,tZHY.[order_sub_no]
           ,tZHY.[age]
           ,tZHY.[is_db]
           ,tZHY.[config_name]
           ,case tZHY.is_db when '是' then  1 else 0 end as [IsPackage]
  FROM [dbo].tZHY as [tZHY]
  inner join tDepartment on tZHY.department_code=tDepartment.PivalId
  inner join tBatch on tZHY.batch = tBatch.batch
  where drug_weight='1' 
  and tZHY.use_date = @use_date
  and not exists(
	select 1 from dbo.tOrder where dbo.tOrder.use_date = tZHY.use_date and dbo.tOrder.group_num = tZHY.group_num and dbo.tOrder.batch = tZHY.batch and  dbo.tOrder.use_time = tZHY.use_time 
  )
END
GO
/****** Object:  StoredProcedure [dbo].[p_backupZHY]    Script Date: 02/25/2019 18:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[p_backupZHY]
AS
BEGIN
	insert into tZHYBak
	select * from tZHY
	where use_date<CONVERT(varchar(100), DateAdd(dd,-1,getdate()), 23)

	delete tZHY where use_date<CONVERT(varchar(100), DateAdd(dd,-1,getdate()), 23)
END
GO
/****** Object:  StoredProcedure [dbo].[p_backupOrder]    Script Date: 02/25/2019 18:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_backupOrder] 
AS
BEGIN
	insert into tOrderBak
	select * from tOrder
	where use_date<CONVERT(varchar(100), DateAdd(dd,-1,getdate()), 23)

	delete tOrder where use_date<CONVERT(varchar(100), DateAdd(dd,-1,getdate()), 23)
END
GO
/****** Object:  Default [DF__tOrderSummar__Id__239E4DCF]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF__tOrderSum__Total__24927208]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [TotalCount]
GO
/****** Object:  Default [DF__tOrderSum__AutoC__25869641]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [AutoCount]
GO
/****** Object:  Default [DF__tOrderSum__Manua__267ABA7A]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [ManualCount]
GO
/****** Object:  Default [DF__tOrderSum__HuaLi__276EDEB3]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [HuaLiaoCount]
GO
/****** Object:  Default [DF__tOrderSum__YingY__286302EC]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [YingYangYeCount]
GO
/****** Object:  Default [DF__tOrderSum__Packa__29572725]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [PackageCount]
GO
/****** Object:  Default [DF__tOrderSum__Avoid__2A4B4B5E]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [AvoidLightCount]
GO
/****** Object:  Default [DF__tOrderSum__Other__2B3F6F97]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderSummary] ADD  DEFAULT ((0)) FOR [OtherCount]
GO
/****** Object:  Default [DF_tOrderBak_Id]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrderBak] ADD  CONSTRAINT [DF_tOrderBak_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_tOrder_Id]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tOrder] ADD  CONSTRAINT [DF_tOrder_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF__tDeliverySum__Id__2DE6D218]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tDeliverySummary] ADD  CONSTRAINT [DF__tDeliverySum__Id__2DE6D218]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_tZHYBak_Id]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tZHYBak] ADD  CONSTRAINT [DF_tZHYBak_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_tZHY_Id]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tZHY] ADD  CONSTRAINT [DF_tZHY_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF__tWarningSumm__Id__2C3393D0]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tWarningSummary] ADD  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  ForeignKey [FK_TUSERROL_REFERENCE_TROLE]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tUserRole]  WITH CHECK ADD  CONSTRAINT [FK_TUSERROL_REFERENCE_TROLE] FOREIGN KEY([RoleId])
REFERENCES [dbo].[tRole] ([Id])
GO
ALTER TABLE [dbo].[tUserRole] CHECK CONSTRAINT [FK_TUSERROL_REFERENCE_TROLE]
GO
/****** Object:  ForeignKey [FK_TUSERROL_REFERENCE_TUSER]    Script Date: 02/25/2019 18:19:21 ******/
ALTER TABLE [dbo].[tUserRole]  WITH CHECK ADD  CONSTRAINT [FK_TUSERROL_REFERENCE_TUSER] FOREIGN KEY([UserId])
REFERENCES [dbo].[tUser] ([Id])
GO
ALTER TABLE [dbo].[tUserRole] CHECK CONSTRAINT [FK_TUSERROL_REFERENCE_TUSER]
GO
