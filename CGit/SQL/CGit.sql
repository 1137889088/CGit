-- ================================================

-- Author: 陈磊

-- Create date: 2017-5-2

-- Description: CGit数据库表结构

-- ================================================

/*==============================================================*/
/* Table: 用户表                                                */
/*==============================================================*/
CREATE TABLE [CGit].[users] (
	[email] varchar(20) COLLATE Chinese_PRC_CI_AS NOT NULL ,--用户邮箱
	[name] varchar(20) COLLATE Chinese_PRC_CI_AS NULL ,--用户名
	[pwd] varchar(20) COLLATE Chinese_PRC_CI_AS NULL ,--用户密码
	[area] varchar(20) COLLATE Chinese_PRC_CI_AS NULL ,--用户位置
	[resume] varchar(100) COLLATE Chinese_PRC_CI_AS NULL ,--用户简介
	CONSTRAINT [pk_users] PRIMARY KEY ([email])
)
ON [PRIMARY]
GO


/*==============================================================*/
/* Table: 仓库表                                                */
/*==============================================================*/
CREATE TABLE CGit.repository (
	repository_id int IDENTITY(1,1) primary key ,--仓库id
	email varchar(20), --所属用户的email
	repository_name varchar(20),--仓库名
	repository_describe varchar(50),--仓库描述
	repository_language varchar(20)--仓库语言
)


/*==============================================================*/
/* Table: 评论表                                                */
/*==============================================================*/
CREATE TABLE CGit.comment (
	comment_id INT,--评论id
	comment_email VARCHAR(20),--评论人email
	user_email varchar(20),--被评论人email
	content varchar(200),--评论内容
)


/*==============================================================*/
/* Table: 关注仓库                                              */
/*==============================================================*/
CREATE TABLE CGit.followRepository(
	user_email varchar(20),--关注者email
	follow_email varchar(20)--被关注者email
)


/*==============================================================*/
/* Table: 关注表                                                */
/*==============================================================*/
CREATE TABLE CGit.followUser(
	user_email varchar(20),--关注者email
	follow_email varchar(20)--被关注者email
)


/*==============================================================*/
/* Table: 版本表                                                */
/*==============================================================*/
CREATE TABLE [CGit].[version] (
[version_id] int NOT NULL IDENTITY(1,1) ,--版本id
[repository_id] int NULL ,--所属仓库id
[version_identity] varchar(20) COLLATE Chinese_PRC_CI_AS NULL ,--版本标识符
[version_date] varchar(20) COLLATE Chinese_PRC_CI_AS NULL ,--版本最后修改日期
CONSTRAINT [PK__version__07A588693A309249] PRIMARY KEY ([version_id])
)
ON [PRIMARY]
GO