USE [master]
GO
/****** Object:  Database [PrintTagDb]    Script Date: 02/25/2019 13:40:09 ******/
CREATE DATABASE [PrintTagDb] ON  PRIMARY 
( NAME = N'PrintTagDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\PrintTagDb.mdf' , SIZE = 6400KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PrintTagDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\PrintTagDb_log.LDF' , SIZE = 2112KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PrintTagDb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PrintTagDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PrintTagDb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [PrintTagDb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [PrintTagDb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [PrintTagDb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [PrintTagDb] SET ARITHABORT OFF
GO
ALTER DATABASE [PrintTagDb] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [PrintTagDb] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [PrintTagDb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [PrintTagDb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [PrintTagDb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [PrintTagDb] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [PrintTagDb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [PrintTagDb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [PrintTagDb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [PrintTagDb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [PrintTagDb] SET  ENABLE_BROKER
GO
ALTER DATABASE [PrintTagDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [PrintTagDb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [PrintTagDb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [PrintTagDb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [PrintTagDb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [PrintTagDb] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [PrintTagDb] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [PrintTagDb] SET  READ_WRITE
GO
ALTER DATABASE [PrintTagDb] SET RECOVERY FULL
GO
ALTER DATABASE [PrintTagDb] SET  MULTI_USER
GO
ALTER DATABASE [PrintTagDb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [PrintTagDb] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'PrintTagDb', N'ON'
GO
USE [PrintTagDb]
GO
/****** Object:  Table [dbo].[v_users]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v_users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[true_name] [nvarchar](50) NULL,
	[type_name] [nvarchar](50) NULL,
	[createtime] [datetime] NULL,
 CONSTRAINT [PK_v_users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tZHYBak]    Script Date: 02/25/2019 13:40:09 ******/
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
	[is_print_snv] [varchar](23) NULL,
	[barcode] [varchar](100) NULL,
 CONSTRAINT [PK_tZHYBak] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tZHY]    Script Date: 02/25/2019 13:40:09 ******/
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
	[is_print_snv] [varchar](23) NULL,
	[barcode] [varchar](100) NULL,
 CONSTRAINT [PK_tZHY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tWarningBak]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tWarningBak](
	[warningId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[OrderId] [int] NULL,
	[RowNumber] [int] NOT NULL,
	[drug_id] [varchar](20) NULL,
	[ydrug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[ydrug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[ydrug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[ydrug_spec] [varchar](50) NULL,
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
	[printing_status] [int] NULL,
	[printing_model] [int] NULL,
	[printing_time] [datetime] NULL,
	[sbatches] [varchar](20) NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[PrintUserId] [int] NULL,
	[PrintUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[is_print_snv] [varchar](23) NULL,
	[WarningState] [varchar](50) NULL,
	[detection_drug_name] [varchar](100) NULL,
	[detection_drug_spec] [varchar](50) NULL,
	[barcode] [varchar](100) NULL,
 CONSTRAINT [PK_tWarningBak] PRIMARY KEY CLUSTERED 
(
	[warningId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品权重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品商品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_spmc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品作用代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_class_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'usage_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'use_org'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'use_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'durg_use_sp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'drug_use_units'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用频次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'use_frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'use_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'use_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'stop_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'start_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱子序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'order_sub_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'order_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长期/临时医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'icatrepeat_indorm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'new_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'婴儿标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'yebz'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊用药提示（先用、半量、冷藏、特殊低速、避光滴注、儿童慎用、18岁以下禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'special_medicationtip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大小规格小计量药品用下划线标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'size_specification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'静配备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'pass_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'患者id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'patient_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医生姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'doctor_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'patient_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'batch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'departmengt_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'department_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'zone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次住院标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'visit_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'group_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'sum_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'低速' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'ml_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'create_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱状态（正常/停药）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'order_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否重打' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'is_twice_print'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'checker'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摆药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'deliveryer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'config_person'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'config_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'usage_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'床位编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'bed_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药篮编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'basket_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'printing_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'printing_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'printing_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'溶媒批号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'sbatches'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'electroni_signature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成品复核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'is_cpfh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否收费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'is_sf'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打包药' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'is_db'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'config_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'异常状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'WarningState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测溶媒名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'detection_drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测溶媒规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarningBak', @level2type=N'COLUMN',@level2name=N'detection_drug_spec'
GO
/****** Object:  Table [dbo].[tWarning]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tWarning](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[RowNumber] [int] NOT NULL,
	[drug_id] [varchar](20) NULL,
	[ydrug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[ydrug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[ydrug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[ydrug_spec] [varchar](50) NULL,
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
	[printing_status] [int] NULL,
	[printing_model] [int] NULL,
	[printing_time] [datetime] NULL,
	[sbatches] [varchar](20) NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[PrintUserId] [int] NULL,
	[PrintUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[is_print_snv] [varchar](23) NULL,
	[WarningState] [varchar](50) NULL,
	[detection_drug_name] [varchar](100) NULL,
	[detection_drug_spec] [varchar](50) NULL,
	[barcode] [varchar](100) NULL,
 CONSTRAINT [PK_tWarning] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品权重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品商品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_spmc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品作用代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_class_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'usage_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'use_org'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'use_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'durg_use_sp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'drug_use_units'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用频次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'use_frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'use_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'use_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'stop_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'start_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱子序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'order_sub_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'order_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长期/临时医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'icatrepeat_indorm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'new_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'婴儿标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'yebz'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊用药提示（先用、半量、冷藏、特殊低速、避光滴注、儿童慎用、18岁以下禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'special_medicationtip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大小规格小计量药品用下划线标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'size_specification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'静配备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'pass_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'患者id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'patient_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医生姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'doctor_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'patient_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'batch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'departmengt_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'department_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'zone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次住院标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'visit_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'group_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'sum_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'低速' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'ml_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'create_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱状态（正常/停药）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'order_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否重打' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'is_twice_print'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'checker'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摆药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'deliveryer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'config_person'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'config_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'usage_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'床位编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'bed_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药篮编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'basket_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'printing_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'printing_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'printing_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'溶媒批号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'sbatches'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'electroni_signature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成品复核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'is_cpfh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否收费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'is_sf'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打包药' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'is_db'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'config_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'异常状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'WarningState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测溶媒名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'detection_drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测溶媒规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tWarning', @level2type=N'COLUMN',@level2name=N'detection_drug_spec'
GO
/****** Object:  Table [dbo].[tUser]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[true_name] [nvarchar](50) NULL,
	[type_name] [nvarchar](50) NULL,
	[createtime] [datetime] NULL,
 CONSTRAINT [PK_tUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tOrderBak]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tOrderBak](
	[bakId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [int] NULL,
	[RowNumber] [int] NOT NULL,
	[drug_id] [varchar](20) NULL,
	[ydrug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[ydrug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[ydrug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[ydrug_spec] [varchar](50) NULL,
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
	[printing_status] [int] NULL,
	[printing_model] [int] NULL,
	[printing_time] [datetime] NULL,
	[sbatches] [varchar](20) NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[PrintUserId] [int] NULL,
	[PrintUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[is_print_snv] [varchar](23) NULL,
	[barcode] [varchar](100) NULL,
 CONSTRAINT [PK_tOrderBak] PRIMARY KEY CLUSTERED 
(
	[bakId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品权重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品商品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_spmc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品作用代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_class_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'usage_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'use_org'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'use_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'durg_use_sp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'drug_use_units'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用频次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'use_frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'use_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'use_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'stop_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'start_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱子序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'order_sub_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'order_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长期/临时医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'icatrepeat_indorm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'new_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'婴儿标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'yebz'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊用药提示（先用、半量、冷藏、特殊低速、避光滴注、儿童慎用、18岁以下禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'special_medicationtip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大小规格小计量药品用下划线标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'size_specification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'静配备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'pass_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'患者id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'patient_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医生姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'doctor_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'patient_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'batch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'departmengt_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'department_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'zone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次住院标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'visit_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'group_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'sum_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'低速' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'ml_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'create_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱状态（正常/停药）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'order_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否重打' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'is_twice_print'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'checker'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摆药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'deliveryer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'config_person'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'config_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'usage_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'床位编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'bed_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药篮编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'basket_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'printing_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'printing_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'printing_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'溶媒批号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'sbatches'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'electroni_signature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成品复核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'is_cpfh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否收费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'is_sf'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打包药' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'is_db'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrderBak', @level2type=N'COLUMN',@level2name=N'config_name'
GO
/****** Object:  Table [dbo].[tOrder]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RowNumber] [int] NOT NULL,
	[drug_id] [varchar](20) NULL,
	[ydrug_id] [varchar](20) NULL,
	[drug_number] [varchar](20) NULL,
	[drug_name] [varchar](100) NULL,
	[ydrug_name] [varchar](100) NULL,
	[drug_weight] [varchar](20) NULL,
	[drug_spmc] [varchar](100) NULL,
	[drug_class_name] [varchar](50) NULL,
	[ydrug_class_name] [varchar](50) NULL,
	[drug_spec] [varchar](50) NULL,
	[ydrug_spec] [varchar](50) NULL,
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
	[printing_status] [int] NULL,
	[printing_model] [int] NULL,
	[printing_time] [datetime] NULL,
	[sbatches] [varchar](20) NULL,
	[electroni_signature] [int] NULL,
	[is_cpfh] [varchar](2) NULL,
	[is_sf] [varchar](2) NULL,
	[age] [varchar](10) NULL,
	[is_db] [varchar](2) NULL,
	[config_name] [varchar](20) NULL,
	[PrintUserId] [int] NULL,
	[PrintUserName] [nvarchar](50) NULL,
	[CheckUserId] [int] NULL,
	[CheckUserName] [nvarchar](50) NULL,
	[is_print_snv] [varchar](23) NULL,
	[barcode] [varchar](100) NULL,
 CONSTRAINT [PK_tOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品权重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品商品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_spmc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品作用代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_class_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药品规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'usage_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'use_org'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'use_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'durg_use_sp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计量单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'drug_use_units'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用频次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'use_frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'use_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'use_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'stop_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始用药时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'start_date_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱子序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'order_sub_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'order_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长期/临时医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'icatrepeat_indorm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新医嘱标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'new_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'婴儿标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'yebz'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊用药提示（先用、半量、冷藏、特殊低速、避光滴注、儿童慎用、18岁以下禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'special_medicationtip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大小规格小计量药品用下划线标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'size_specification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'静配备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'pass_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'患者id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'patient_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医生姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'doctor_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'patient_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'batch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'departmengt_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科室编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'department_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'zone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本次住院标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'visit_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'group_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'sum_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'低速' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'ml_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'create_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'医嘱状态（正常/停药）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'order_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否重打' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'is_twice_print'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'checker'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摆药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'deliveryer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配药人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'config_person'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'config_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用法说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'usage_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'床位编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'bed_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'药篮编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'basket_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'printing_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'printing_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'printing_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'溶媒批号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'sbatches'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'electroni_signature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否成品复核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'is_cpfh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否收费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'is_sf'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打包药' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'is_db'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tOrder', @level2type=N'COLUMN',@level2name=N'config_name'
GO
/****** Object:  Table [dbo].[tDrug]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tDrug](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[drug_code] [varchar](50) NULL,
	[drug_name] [varchar](100) NULL,
	[drug_spec] [varchar](50) NULL,
	[drug_units] [varchar](50) NULL,
	[drug_use_spec] [varchar](22) NULL,
	[drug_use_units] [varchar](50) NULL,
	[drug_form] [varchar](100) NULL,
	[input_code] [varchar](50) NULL,
 CONSTRAINT [PK_tDrug] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tDept]    Script Date: 02/25/2019 13:40:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tDept](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[ShortCut] [varchar](30) NULL,
	[Isuse] [int] NOT NULL,
 CONSTRAINT [PK_tDept] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tBatch]    Script Date: 02/25/2019 13:40:09 ******/
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
 CONSTRAINT [PK_tBatch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[P_UpdatetOrderFromtZHY]    Script Date: 02/25/2019 13:40:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_UpdatetOrderFromtZHY]
	-- Add the parameters for the stored procedure here
	@use_date varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  declare @t_group_num varchar(20)
  declare @t_batch varchar(20)
  declare @t_use_time varchar(20)
  declare @t_drug_spec varchar(20)
  declare @t_special_medicationtip varchar(20)
  declare @t_is_cpfh varchar(20)
  declare @t_is_sf varchar(20)
  declare @t_order_status varchar(20)
  declare @t_is_db varchar(20)

Declare zhyCursor Cursor local  for   
  select group_num,batch,use_time,drug_spec,special_medicationtip,is_cpfh,is_sf,order_status,is_db from tZHY where use_date = @use_date and drug_weight='1'   ---查询语句（查询当前的工班）  
   --打开游标  
  Open zhyCursor   
  --循环并提取记录  
  Fetch Next From zhyCursor Into @t_group_num,@t_batch,@t_use_time,@t_drug_spec,@t_special_medicationtip,@t_is_cpfh,@t_is_sf,@t_order_status,@t_is_db
    While ( @@Fetch_Status=0 )     
        begin  
		update tOrder set drug_spec = @t_drug_spec
		where use_date = @use_date and use_time = @t_use_time and group_num = @t_group_num and batch=@t_batch
		print(@t_drug_spec)
     Fetch Next From zhyCursor into @t_group_num,@t_batch,@t_use_time,@t_drug_spec,@t_special_medicationtip,@t_is_cpfh,@t_is_sf,@t_order_status,@t_is_db
       end   
  --关闭游标     
   Close zhyCursor  
END
GO
/****** Object:  StoredProcedure [dbo].[P_InsertIntotOrderSelecttZHY]    Script Date: 02/25/2019 13:40:10 ******/
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
	SET NOCOUNT ON;

INSERT INTO [dbo].[tOrder]
SELECT 0 as [RowNumber]
	  ,o.[drug_id]
      ,p.drug_id as [drug_id]
      ,o.[drug_number]
      ,o.[drug_name]
	  ,p.drug_name as [ydrug_name]
      ,o.[drug_weight]
      ,o.[drug_spmc]
      ,o.[drug_class_name]
	  ,p.[drug_class_name] as [ydrug_class_name]
      ,o.[drug_spec]
	  ,p.[drug_spec] as [ydrug_spec]
      ,o.[usage_id]
      ,o.[use_org]
      ,o.[use_count]
      ,o.[durg_use_sp]
      ,o.[drug_use_units]
      ,o.[use_frequency]
      ,o.[use_date]
      ,o.[use_time]
      ,o.[stop_date_time]
      ,o.[start_date_time]
      ,o.[order_sub_no]
      ,o.[order_type]
      ,o.[icatrepeat_indorm]
      ,o.[new_orders]
      ,o.[yebz]
      ,o.[special_medicationtip]
      ,o.[size_specification]
      ,o.[pass_remark]
      ,o.[patient_id]
      ,o.[doctor_name]
      ,o.[patient_name]
      ,o.[batch]
      ,o.[departmengt_name]
      ,o.[department_code]
      ,o.[zone]
      ,o.[visit_id]
      ,o.[group_num]
      ,o.[sum_num]
      ,o.[ml_speed]
      ,o.[create_date]
      ,o.[order_status]
      ,o.[is_twice_print]
      ,o.[checker]
      ,o.[deliveryer]
      ,o.[config_person]
      ,o.[config_date]
      ,o.[usage_name]
      ,o.[bed_number]
      ,o.[basket_number]
      ,0 as [printing_status]
      ,null as [printing_model]
      ,null as [printing_time]
      ,null as [sbatches]
      ,o.[electroni_signature]
      ,o.[is_cpfh]
      ,o.[is_sf]
      ,o.[age]
      ,o.[is_db]
      ,o.[config_name]
      ,null as [PrintUserId]
      ,null as [PrintUserName]
      ,null as [CheckUserId]
      ,null as [CheckUserName]
      ,o.[is_print_snv]
      ,o.[barcode]
  FROM [dbo].[tZHY] o
  join dbo.tZHY p on o.group_num = p.group_num and o.use_date = p.use_date and o.batch=p.batch and o.use_time = p.use_time
  where o.use_date = @use_date 
  and o.drug_weight='1' 
  --and p.drug_weight='5'
  and exists (
	select 1 from dbo.tZHY where dbo.tZHY.use_date = o.use_date and dbo.tZHY.group_num = o.group_num and dbo.tZHY.batch = o.batch and  dbo.tZHY.use_time = o.use_time 
	and tZHY.drug_weight<>'1'
	group by tZHY.drug_weight
	having min(tZHY.drug_weight)=p.drug_weight
  )
  and not exists(
select 1 from dbo.tOrder where dbo.tOrder.use_date = o.use_date and dbo.tOrder.group_num = o.group_num and dbo.tOrder.batch = o.batch and  dbo.tOrder.use_time = o.use_time 
  )
END
GO
/****** Object:  StoredProcedure [dbo].[P_BakHistoryData]    Script Date: 02/25/2019 13:40:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_BakHistoryData]
	-- Add the parameters for the stored procedure here
	@use_date varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tzhybak
select * from tZHY
where use_date<@use_date

delete tZHY
where use_date<@use_date

insert into tOrderbak
select * from tOrder
where use_date<@use_date

delete tOrder
where use_date<@use_date

insert into tWarningbak
select * from tWarning
where use_date<@use_date

delete tWarning
where use_date<@use_date
END
GO
/****** Object:  Default [DF_tWarningBak_printing_status]    Script Date: 02/25/2019 13:40:09 ******/
ALTER TABLE [dbo].[tWarningBak] ADD  CONSTRAINT [DF_tWarningBak_printing_status]  DEFAULT ((0)) FOR [printing_status]
GO
/****** Object:  Default [DF_tWarning_printing_status]    Script Date: 02/25/2019 13:40:09 ******/
ALTER TABLE [dbo].[tWarning] ADD  CONSTRAINT [DF_tWarning_printing_status]  DEFAULT ((0)) FOR [printing_status]
GO
/****** Object:  Default [DF_tOrderBak_printing_status]    Script Date: 02/25/2019 13:40:09 ******/
ALTER TABLE [dbo].[tOrderBak] ADD  CONSTRAINT [DF_tOrderBak_printing_status]  DEFAULT ((0)) FOR [printing_status]
GO
/****** Object:  Default [DF_tOrder_printing_status]    Script Date: 02/25/2019 13:40:09 ******/
ALTER TABLE [dbo].[tOrder] ADD  CONSTRAINT [DF_tOrder_printing_status]  DEFAULT ((0)) FOR [printing_status]
GO
