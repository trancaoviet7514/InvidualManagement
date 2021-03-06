USE [InvidualManager]
GO
/****** Object:  StoredProcedure [dbo].[usp_Seach]    Script Date: 1/20/2018 2:47:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter proc [dbo].[usp_Seach]
@LoaiChiTieu nvarchar(50), @NoiDung nvarchar(50), @From datetime, @To datetime , @Groupby nvarchar(20), @GB_LoaiChiTieu nvarchar(1), @GB_NoiDung nvarchar(1)
as
begin
	--B2: xóa dữ liệu từ bảng cũ
	delete from ChiTiet_Tam
	--B1: Lọc dữ liệu
	if(@NoiDung ='')
	begin
		if(@LoaiChiTieu= '' or @LoaiChiTieu= N'Tất cả')
		begin
			insert into ChiTiet_Tam select * from ChiTiet where Thoigian<=@To and Thoigian >= @From
			
		end
		if(@LoaiChiTieu != '' and @LoaiChiTieu != N'Tất cả')
		begin
			insert into ChiTiet_Tam select * from ChiTiet where Thoigian<=@To and Thoigian >= @From and LoaiChiTieu = @LoaiChiTieu
			
		end
	end
	if(@NoiDung!='')
	begin
		insert into ChiTiet_Tam select * from ChiTiet where Thoigian<=@To and Thoigian >= @From and Noidung = @NoiDung
	end
	--B3: Group by
	if((@Groupby = '' and @GB_LoaiChiTieu = '0' and @GB_NoiDung = '0') or (@Groupby != '' and @GB_LoaiChiTieu != '0' and @GB_NoiDung != '0'))
	begin
		select ThoiGian as N'Ngày',LoaiChiTieu N'Loại chi tiêu', NoiDung N'Nội dung',  SoTien N'Số tiền' from ChiTiet_Tam
		return
	end
	if(@Groupby != '' and @GB_LoaiChiTieu = '0' and @GB_NoiDung = '0')
	begin
		if(@Groupby=N'Ngày')
		begin
			select ThoiGian as N'Ngày',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by ThoiGian
			order by ThoiGian asc
			return
		end
		if(@Groupby=N'Tuần')
		begin
			select DATEPART(week,ThoiGian) as N'Tuần',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by DATEPART(week,ThoiGian)
			order by DATEPART(week,ThoiGian) asc
			return
		end
		if(@Groupby=N'Tháng')
		begin
			select DATEPART(month,ThoiGian) as N'Tháng',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by DATEPART(month,ThoiGian)
			order by DATEPART(month,ThoiGian) asc
			return
		end
		return
	end
	if(@Groupby = '' and @GB_LoaiChiTieu != '0' and @GB_NoiDung = '0')
	begin
		select LoaiChiTieu, sum(cast(SoTien as int)) as N'Số tiền' from ChiTiet_Tam group by LoaiChiTieu
		return
	end
	if(@Groupby = '' and @GB_LoaiChiTieu = '0' and @GB_NoiDung != '0')
	begin
		select NoiDung, sum(cast(SoTien as int)) as N'Số tiền' from ChiTiet_Tam group by NoiDung
		return
	end
	if(@Groupby != '' and @GB_LoaiChiTieu != '0' and @GB_NoiDung = '0')
	begin
		if(@Groupby=N'Ngày')
		begin
			select ThoiGian as N'Ngày', LoaiChiTieu as N'Loại chi tiêu',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by ThoiGian, LoaiChiTieu
			order by ThoiGian asc
			return
		end
		if(@Groupby=N'Tuần')
		begin
			select DATEPART(week,ThoiGian) as N'Tuần', LoaiChiTieu as N'Loại chi tiêu',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by DATEPART(week,ThoiGian), LoaiChiTieu
			order by DATEPART(week,ThoiGian) asc
			return
		end
		if(@Groupby=N'Tháng')
		begin
			select DATEPART(month,ThoiGian) as N'Tháng', LoaiChiTieu as N'Loại chi tiêu',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by DATEPART(month,ThoiGian), LoaiChiTieu
			order by DATEPART(month,ThoiGian) asc
			return
		end
		return
	end
	if(@Groupby != '' and @GB_LoaiChiTieu = '0' and @GB_NoiDung != '0')
	begin
		if(@Groupby=N'Ngày')
		begin
			select ThoiGian as N'Ngày',NoiDung as N'Nội dung',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by ThoiGian,NoiDung
			order by ThoiGian asc
			return
		end
		if(@Groupby=N'Tuần')
		begin
			select DATEPART(week,ThoiGian) as N'Tuần',NoiDung as N'Nội dung',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by DATEPART(week,ThoiGian),NoiDung
			order by DATEPART(week,ThoiGian) asc
			return
		end
		if(@Groupby=N'Tháng')
		begin
			select DATEPART(month,ThoiGian) as N'Tháng',NoiDung as N'Nội dung',sum(cast(SoTien as int)) as N'Số tiền'
			from ChiTiet_Tam
			group by DATEPART(month,ThoiGian),NoiDung
			order by DATEPART(month,ThoiGian) asc
			return
		end
		return
	end
	if(@Groupby = '' and @GB_LoaiChiTieu != '0' and @GB_NoiDung != '0')
	begin
		select LoaiChiTieu as N'Loại chi tiêu', NoiDung as N'Nội dung', sum(cast(SoTien as int)) as N'Số tiền' from ChiTiet_Tam
		group by LoaiChiTieu, NoiDung
		return
	end
end

GO
