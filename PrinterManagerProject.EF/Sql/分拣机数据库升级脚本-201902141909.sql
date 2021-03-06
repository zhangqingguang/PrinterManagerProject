USE [PrintTagDb]
GO
/****** Object:  StoredProcedure [dbo].[p_getMainDrugList]    Script Date: 2019/3/14 19:08:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	获取当日批次的主药
-- =============================================
CREATE PROCEDURE [dbo].[p_getMainDrugList]
	-- Add the parameters for the stored procedure here
	@use_date varchar(20),
	@batch varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


	/*
		declare @use_date varchar(20)='2019-03-02'
	declare @batch varchar(20)='01'
	--*/
	select tDrug.* from (select distinct ydrug_id as ydrug_id from tOrder where use_date = @use_date and batch = @batch)a
	join tDrug on a.ydrug_id=tDrug.drug_code
END




GO
/****** Object:  StoredProcedure [dbo].[P_InsertIntotOrderSelecttZHY]    Script Date: 2019/3/14 19:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[P_InsertIntotOrderSelecttZHY] 
	-- Add the parameters for the stored procedure here
	@use_date varchar(20),
	@batch varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[tOrder]

           ([RowNumber]
           ,[drug_id]
           ,[ydrug_id]
           ,[drug_number]
           ,[drug_name]
           ,[ydrug_name]
           ,[drug_weight]
           ,[drug_spmc]
           ,[drug_class_name]
           ,[ydrug_class_name]
           ,[drug_spec]
           ,[ydrug_spec]
           ,[usage_id]
           ,[use_org]
           ,[use_count]
           ,[durg_use_sp]
           ,[drug_use_units]
           ,[use_frequency]
           ,[use_date]
           ,[use_time]
           ,[stop_date_time]
           ,[start_date_time]
           ,[order_sub_no]
           ,[order_type]
           ,[icatrepeat_indorm]
           ,[new_orders]
           ,[yebz]
           ,[special_medicationtip]
           ,[size_specification]
           ,[pass_remark]
           ,[patient_id]
           ,[doctor_name]
           ,[patient_name]
           ,[batch]
           ,[batch_name]
           ,[departmengt_name]
           ,[department_code]
           ,[zone]
           ,[visit_id]
           ,[group_num]
           ,[sum_num]
           ,[ml_speed]
           ,[create_date]
           ,[order_status]
           ,[is_twice_print]
           ,[checker]
           ,[deliveryer]
           ,[config_person]
           ,[config_date]
           ,[usage_name]
           ,[bed_number]
           ,[basket_number]
           ,[printing_status]
           ,[printing_model]
           ,[printing_time]
           ,[sbatches]
           ,[electroni_signature]
           ,[is_cpfh]
           ,[is_sf]
           ,[age]
           ,[is_db]
           ,[config_name]
           ,[PrintUserId]
           ,[PrintUserName]
           ,[CheckUserId]
           ,[CheckUserName]
           ,[is_print_snv]
           ,[barcode]
           ,[sex]
           ,[xsyxj])
SELECT 0 as [RowNumber]
	  ,o.[drug_id]
      ,null as [ydrug_id]
      ,o.[drug_number]
      ,o.[drug_name]
	  ,null as [ydrug_name]
      ,o.[drug_weight]
      ,o.[drug_spmc]
      ,o.[drug_class_name]
	  ,null as [ydrug_class_name]
      ,o.[drug_spec]
	  ,null as [ydrug_spec]
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
      ,b.[batch_name]
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
      ,o.[sex]
      ,o.[xsyxj]
  FROM [dbo].[tZHY] o
  --join (
  --select * from dbo.tZHY where use_date = @use_date and batch=@batch and drug_weight!=1
  --order by 
  --group by use_date,batch,group_num,use_time
  --) p on o.group_num = p.group_num and o.use_date = p.use_date and o.batch=p.batch and o.use_time = p.use_time
  join dbo.tBatch b on b.batch=o.batch
  where o.use_date = @use_date 
  and o.batch = @batch
  and o.drug_weight='1' 
  --and p.drug_weight='5'
  and exists (
	select 1 from dbo.tZHY where dbo.tZHY.use_date = o.use_date and dbo.tZHY.group_num = o.group_num and dbo.tZHY.batch = o.batch and  dbo.tZHY.use_time = o.use_time 
	and tZHY.drug_weight<>'1'
	--group by tZHY.drug_weight
	--having min(tZHY.drug_weight)=p.drug_weight
  )
  and not exists(
	select 1 from dbo.tOrder where dbo.tOrder.use_date = o.use_date and dbo.tOrder.group_num = o.group_num and dbo.tOrder.batch = o.batch and  dbo.tOrder.use_time = o.use_time 
  )


  -- 修改主药信息
  
  declare @orderId int
  declare @use_time varchar(20)
  declare @group_num varchar(20)
  declare @drug_id varchar(20)
  declare @drug_name varchar(100)
  declare @drug_class_name varchar(50)
  declare @drug_spec varchar(50)

  select @orderId=count(*) from tOrder where use_date = @use_date and batch=@batch and ydrug_id is null
  print(@orderId)

Declare mainDrugCursor Cursor local  for   
  select id,use_time,group_num from tOrder where use_date = @use_date and batch=@batch and ydrug_id is null   ---查询语句（查询当前的工班）  
   --打开游标  
  Open mainDrugCursor   
	--循环并提取记录  
	Fetch Next From mainDrugCursor Into @orderId,@use_time,@group_num
    While ( @@Fetch_Status=0 )     
        begin  

		-- 查找主药（按照sum_num排序，第一个时溶媒，第二个就是主药）
		select top 1 @drug_id =drug_id,@drug_name =drug_name,@drug_class_name  =drug_class_name,@drug_spec=drug_spec
		from tZHY
		where use_date = @use_date and use_time = @use_time and group_num = @group_num and batch=@batch and drug_weight!='1'
		order by sum_num asc

		--修改
		update tOrder set ydrug_class_name = @drug_class_name,ydrug_id=@drug_id,ydrug_name=@drug_name,ydrug_spec=@drug_spec
		where id=@orderId

		--清空
		set @drug_id =null
		set @drug_name =null
		set @drug_class_name  =null
		set @drug_spec=null

	Fetch Next From mainDrugCursor Into @orderId,@use_time,@group_num
       end   
  --关闭游标     
   Close mainDrugCursor  
END



GO
/****** Object:  StoredProcedure [dbo].[P_InsertIntotOrderSelecttZHY_20190314]    Script Date: 2019/3/14 19:09:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[P_InsertIntotOrderSelecttZHY_20190314] 
	-- Add the parameters for the stored procedure here
	@use_date varchar(20),
	@batch varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[tOrder]

           ([RowNumber]
           ,[drug_id]
           ,[ydrug_id]
           ,[drug_number]
           ,[drug_name]
           ,[ydrug_name]
           ,[drug_weight]
           ,[drug_spmc]
           ,[drug_class_name]
           ,[ydrug_class_name]
           ,[drug_spec]
           ,[ydrug_spec]
           ,[usage_id]
           ,[use_org]
           ,[use_count]
           ,[durg_use_sp]
           ,[drug_use_units]
           ,[use_frequency]
           ,[use_date]
           ,[use_time]
           ,[stop_date_time]
           ,[start_date_time]
           ,[order_sub_no]
           ,[order_type]
           ,[icatrepeat_indorm]
           ,[new_orders]
           ,[yebz]
           ,[special_medicationtip]
           ,[size_specification]
           ,[pass_remark]
           ,[patient_id]
           ,[doctor_name]
           ,[patient_name]
           ,[batch]
           ,[batch_name]
           ,[departmengt_name]
           ,[department_code]
           ,[zone]
           ,[visit_id]
           ,[group_num]
           ,[sum_num]
           ,[ml_speed]
           ,[create_date]
           ,[order_status]
           ,[is_twice_print]
           ,[checker]
           ,[deliveryer]
           ,[config_person]
           ,[config_date]
           ,[usage_name]
           ,[bed_number]
           ,[basket_number]
           ,[printing_status]
           ,[printing_model]
           ,[printing_time]
           ,[sbatches]
           ,[electroni_signature]
           ,[is_cpfh]
           ,[is_sf]
           ,[age]
           ,[is_db]
           ,[config_name]
           ,[PrintUserId]
           ,[PrintUserName]
           ,[CheckUserId]
           ,[CheckUserName]
           ,[is_print_snv]
           ,[barcode]
           ,[sex]
           ,[xsyxj])
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
      ,b.[batch_name]
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
      ,o.[sex]
      ,o.[xsyxj]
  FROM [dbo].[tZHY] o
  join dbo.tZHY p on o.group_num = p.group_num and o.use_date = p.use_date and o.batch=p.batch and o.use_time = p.use_time
  join dbo.tBatch b on b.batch=o.batch
  where o.use_date = @use_date 
  and o.batch = @batch
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
/****** Object:  StoredProcedure [dbo].[P_UpdatetOrderFromtZHY]    Script Date: 2019/3/14 19:09:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[P_UpdatetOrderFromtZHY]
	-- Add the parameters for the stored procedure here
	@use_date varchar(20),
	@batch varchar(20)
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
  select group_num,batch,use_time,drug_spec,special_medicationtip,is_cpfh,is_sf,order_status,is_db from tZHY where use_date = @use_date and batch=@batch and drug_weight='1'   ---查询语句（查询当前的工班）  
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
