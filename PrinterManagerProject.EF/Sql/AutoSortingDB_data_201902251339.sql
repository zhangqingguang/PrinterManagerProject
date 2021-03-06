USE [AutoSortingDB]
GO
/****** Object:  Table [dbo].[tUser]    Script Date: 02/25/2019 13:42:53 ******/
SET IDENTITY_INSERT [dbo].[tUser] ON
INSERT [dbo].[tUser] ([Id], [Account], [Name], [Password], [IsEnabled], [JobNum]) VALUES (1, N'admin', N'admin', N'888888', 1, N'10000')
INSERT [dbo].[tUser] ([Id], [Account], [Name], [Password], [IsEnabled], [JobNum]) VALUES (2, N'10001', N'自动1', N'000000', 1, N'10001')
INSERT [dbo].[tUser] ([Id], [Account], [Name], [Password], [IsEnabled], [JobNum]) VALUES (3, N'10002', N'手动', N'000000', 1, N'10002')
INSERT [dbo].[tUser] ([Id], [Account], [Name], [Password], [IsEnabled], [JobNum]) VALUES (4, N'10003', N'审核', N'000000', 1, N'10003')
SET IDENTITY_INSERT [dbo].[tUser] OFF
/****** Object:  Table [dbo].[tRole]    Script Date: 02/25/2019 13:42:53 ******/
SET IDENTITY_INSERT [dbo].[tRole] ON
INSERT [dbo].[tRole] ([Id], [Name], [Code], [IsAdmin]) VALUES (1, N'管理员', N'Admin', 1)
INSERT [dbo].[tRole] ([Id], [Name], [Code], [IsAdmin]) VALUES (2, N'自动分拣人', N'AutoCollector', 0)
INSERT [dbo].[tRole] ([Id], [Name], [Code], [IsAdmin]) VALUES (3, N'手动分拣人', N'MenuCollector', 0)
INSERT [dbo].[tRole] ([Id], [Name], [Code], [IsAdmin]) VALUES (4, N'分拣复合人', N'Checker', 0)
SET IDENTITY_INSERT [dbo].[tRole] OFF
/****** Object:  Table [dbo].[tDepartment]    Script Date: 02/25/2019 13:42:53 ******/
SET IDENTITY_INSERT [dbo].[tDepartment] ON
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (1, N'避光药', N'10', N'避光药', 100, N'1')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (3, N'产科一病区', N'11994', N'产科一病区', 2, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (4, N'产科二病区', N'11995', N'产科二病区', 3, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (5, N'妇科一病区', N'11999', N'妇科一病区', 4, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (6, N'妇科二病区', N'12000', N'妇科二病区', 5, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (7, N'创伤外科', N'12001', N'创伤外科', 6, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (8, N'神经外科一', N'12002', N'神经外科一病区', 7, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (9, N'泌尿外科', N'12007', N'泌尿外科', 8, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (10, N'胃肠外科', N'12008', N'胃肠外科', 9, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (11, N'护理部', N'12130', N'护理部', 10, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (12, N'急诊ICU', N'12166', N'急诊重症监护病区', 11, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (13, N'急诊病区', N'12167', N'急诊病区', 12, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (14, N'健康中心化验室', N'12510', N'健康中心化验室', 13, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (15, N'肝胆胰外科', N'12727', N'肝胆胰外科', 14, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (16, N'核医学病区', N'12850', N'核医学病区', 15, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (17, N'感染科呼吸', N'13070', N'感染性疾病科呼吸病区', 16, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (18, N'感染科消化', N'13071', N'感染性疾病科消化病区', 17, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (19, N'急诊留观室', N'13390', N'急诊留观室', 18, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (20, N'血液净化科', N'13670', N'血液净化科', 19, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (21, N'重症内科', N'13672', N'重症医学科内科', 20, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (22, N'新生儿NICU', N'13674', N'新生儿重症监护室NICU', 21, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (23, N'儿童PICU', N'13675', N'儿童重症监护室PICU', 22, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (24, N'儿科', N'13676', N'儿科', 23, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (25, N'神经内科一', N'13677', N'神经内科一病区', 24, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (26, N'神经内科二', N'13678', N'神经内科二病区', 25, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (27, N'康复医学科', N'13679', N'康复医学科', 26, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (28, N'呼吸危重科', N'13680', N'呼吸与危重症医学科', 27, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (29, N'冠心病CCU', N'13682', N'冠心病监护病房CCU', 28, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (30, N'心血管内科', N'13683', N'心血管内科', 29, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (31, N'肾脏内科', N'13684', N'肾脏内科', 30, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (32, N'中医科', N'13685', N'中医科', 31, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (33, N'疼痛科', N'13686', N'疼痛科', 32, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (34, N'血液科一', N'13687', N'血液科一病区', 33, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (35, N'血液科二', N'13688', N'血液科二病区', 34, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (36, N'消化内科', N'13689', N'消化内科', 35, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (37, N'内分泌风湿', N'13690', N'内分泌风湿免疫科', 36, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (38, N'内分泌科', N'13691', N'内分泌科', 37, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (39, N'全科医学科', N'13692', N'全科医学科', 38, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (40, N'产科三病区', N'13694', N'产科三病区', 39, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (41, N'血管外科', N'13695', N'血管外科', 40, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (42, N'眼科', N'13696', N'眼科', 41, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (43, N'耳鼻喉科', N'13697', N'耳鼻喉科', 42, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (44, N'口腔颌面外', N'13698', N'口腔颌面外科', 43, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (45, N'甲、乳外科', N'13699', N'甲状腺、乳腺肿瘤外科', 44, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (46, N'重症外科', N'13710', N'重症医学科外科', 45, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (47, N'胸外科', N'13711', N'胸外科', 46, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (48, N'小儿外科', N'13712', N'小儿外科', 47, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (49, N'神经外科二', N'13730', N'神经外科二病区', 48, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (50, N'产科OICU', N'13910', N'产科重症监护室OICU', 49, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (51, N'药物I期病房', N'14010', N'药物I期临床试验病房', 50, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (52, N'介入科', N'14030', N'介入科', 51, N'')
INSERT [dbo].[tDepartment] ([Id], [ShortName], [PivalId], [PivasName], [BasketNum], [Zone]) VALUES (53, N'血液科移植', N'14130', N'血液科骨髓移植病区', 52, N'')
SET IDENTITY_INSERT [dbo].[tDepartment] OFF
/****** Object:  Table [dbo].[tConfig]    Script Date: 02/25/2019 13:42:53 ******/
SET IDENTITY_INSERT [dbo].[tConfig] ON
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (1, N'AutoCollecterLogin', N'自动分拣人登录', N'1', N'Login')
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (2, N'MenuCollecterLogin', N'手动分拣人登陆', N'0', N'Login')
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (3, N'CheckerLogin', N'分拣复核人登录', N'0', N'Login')
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (4, N'AutoSerialPort', N'自动扫码枪串口', N'com4', N'SerialPort')
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (5, N'MenuSerialPort', N'手动扫码枪串口', N'com5', N'SerialPort')
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (6, N'MainboardSerialPort', N'主板通讯串口号', N'com3', N'SerialPort')
INSERT [dbo].[tConfig] ([Id], [Code], [Name], [Value], [GroupCode]) VALUES (7, N'AvoidLightBasketNum', N'避光药药篮编号', N'100', N'AvoidLightBasketNum')
SET IDENTITY_INSERT [dbo].[tConfig] OFF
/****** Object:  Table [dbo].[tBatch]    Script Date: 02/25/2019 13:42:53 ******/
SET IDENTITY_INSERT [dbo].[tBatch] ON
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (1, N'01', N'1批', N'08:00:00', N'09:00:00')
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (2, N'02', N'2批', N'09:00:01', N'10:00:00')
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (3, N'03', N'3批', N'10:00:01', N'12:00:00')
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (7, N'08', N'4批', N'12:00:01', N'16:00:00')
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (8, N'P00', N'凌晨打包', N'00:00:00', N'07:59:59')
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (9, N'P01', N'打包批', N'16:00:01', N'23:59:59')
INSERT [dbo].[tBatch] ([Id], [batch], [batch_name], [start_time], [end_time]) VALUES (10, N'P02', N'特殊批', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tBatch] OFF
/****** Object:  Table [dbo].[tUserRole]    Script Date: 02/25/2019 13:42:53 ******/
SET IDENTITY_INSERT [dbo].[tUserRole] ON
INSERT [dbo].[tUserRole] ([Id], [RoleId], [UserId]) VALUES (1, 1, 1)
INSERT [dbo].[tUserRole] ([Id], [RoleId], [UserId]) VALUES (2, 2, 2)
INSERT [dbo].[tUserRole] ([Id], [RoleId], [UserId]) VALUES (3, 3, 3)
INSERT [dbo].[tUserRole] ([Id], [RoleId], [UserId]) VALUES (5, 4, 4)
SET IDENTITY_INSERT [dbo].[tUserRole] OFF
