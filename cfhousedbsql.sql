USE [TheCoffeeHouse]
GO


INSERT INTO [dbo].[TbCategories] ([Name]) VALUES 
	(N'Cà phê'),
	(N'Matcha'),
	(N'Trà trái cây'),
	(N'Trà sữa'),
	(N'Bánh ngọt'),
	(N'Bánh mặn')
	
GO

select * from TbCategories


INSERT INTO [dbo].[TbProducts] ([Name],[Price],[Description],[ImageUrl],[Notes],[CategoryId])
     VALUES
           (N'Espresso Nóng',45000.0000,N'Một tách Espresso nguyên bản được bắt đầu bởi những hạt Arabica chất lượng, phối trộn với tỉ lệ cân đối hạt Robusta, cho ra vị ngọt caramel, vị chua dịu và sánh đặc.',N'espresso_hot.png',null, 1),
		   (N'Espresso Đá',49000.0000,N'Một tách Espresso nguyên bản được bắt đầu bởi những hạt Arabica chất lượng, phối trộn với tỉ lệ cân đối hạt Robusta, cho ra vị ngọt caramel, vị chua dịu và sánh đặc.',N'espresso_da.png',null, 1),
		   (N'Americano Nóng',45000.0000,N'Americano được pha chế bằng cách pha thêm nước với tỷ lệ nhất định vào tách cà phê Espresso, từ đó mang lại hương vị nhẹ nhàng và giữ trọn được mùi hương cà phê đặc trưng.',N'americano_nong.png',null, 1),
		   (N'A-Mê Đào',49000.0000,N'Mê ly với Americano từ 100% Arabica kết hợp cùng Đào ngọt thanh, dậy vị tươi mát.',N'a-me_dao.png',null, 1),
		   (N'A-Mê Mơ',49000.0000,N'Mê say với Americano từ 100% Arabica kết hợp cùng Mơ chua ngọt, tươi mới mỗi ngày. *Khuấy đều để thưởng thức trọn vị',N'a-me_mo.png',null, 1),
		   (N'Latte Nóng',59000.0000,N'Một sự kết hợp tinh tế giữa vị đắng cà phê Espresso nguyên chất hòa quyện cùng vị sữa nóng ngọt ngào, bên trên là một lớp kem mỏng nhẹ tạo nên một tách cà phê hoàn hảo về hương vị lẫn nhãn quan.',N'latte_nong.png',null, 1),
		   (N'Latte Hạnh Nhân',59000.0000,N'Latte Hạnh Nhân là sự kết hợp giữa Espresso đậm đà, sữa tươi béo ngậy, và syrup hạnh nhân thơm lừng, mang đến một hương vị ngọt nhẹ, béo ngậy và mát lạnh. Thức uống này tạo nên sự hòa quyện tuyệt vời giữa cà phê và vị hạnh nhân đặc trưng, đem lại cảm giác dễ chịu và độc đáo.',N'latte_hanh_nhan.png',null, 1),
		   (N'Latte Classic',55000.0000,N'Latte Đá là sự kết hợp hoàn hảo giữa espresso đậm đà và sữa tươi béo ngậy, tạo ra một hương vị dịu nhẹ, không quá đắng nhưng vẫn giữ được sự đậm đà của cà phê. Khi thêm đá lạnh, Latte Đá mang lại cảm giác mát lạnh, dễ uống, rất thích hợp cho những ngày nóng',N'latte-classic.png',null, 1),
		   (N'Bạc Xỉu',39000.0000,N'Bạc xỉu chính là "Ly sữa trắng kèm một chút cà phê". Thức uống này rất phù hợp những ai vừa muốn trải nghiệm chút vị đắng của cà phê vừa muốn thưởng thức vị ngọt béo ngậy từ sữa.',N'bac_xiu.png',null, 1),
		   (N'Bạc Xỉu Nóng',39000.0000,N'Bạc xỉu chính là "Ly sữa trắng kèm một chút cà phê". Thức uống này rất phù hợp những ai vừa muốn trải nghiệm chút vị đắng của cà phê vừa muốn thưởng thức vị ngọt béo ngậy từ sữa.',N'bac_xiu_nong.png',null, 1),
		   (N'Cà Phê Đen Nóng',39000.0000,N'Không ngọt ngào như Bạc sỉu hay Cà phê sữa, Cà phê đen mang trong mình phong vị trầm lắng, thi vị hơn. Người ta thường phải ngồi rất lâu mới cảm nhận được hết hương thơm ngào ngạt, phảng phất mùi cacao và cái đắng mượt mà trôi tuột xuống vòm họng.',N'ca_phe_phin_den_nong.png',null, 1),
		   (N'Cà Phê Sữa Nóng',39000.0000,N'Cà phê được pha phin truyền thống kết hợp với sữa đặc tạo nên hương vị đậm đà, hài hòa giữa vị ngọt đầu lưỡi và vị đắng thanh thoát nơi hậu vị.',N'ca_phe_phin_nau_nong.png',null, 1),
		   (N'Cà Phê Đen Đá',39000.0000,N'Không ngọt ngào như Bạc sỉu hay Cà phê sữa, Cà phê đen mang trong mình phong vị trầm lắng, thi vị hơn. Người ta thường phải ngồi rất lâu mới cảm nhận được hết hương thơm ngào ngạt, phảng phất mùi cacao và cái đắng mượt mà trôi tuột xuống vòm họng.',N'ca_phe_phin_den_da.png',null, 1),
		   (N'Cà Phê Sữa Đá',39000.0000,N'Cà phê Đắk Lắk nguyên chất được pha phin truyền thống kết hợp với sữa đặc tạo nên hương vị đậm đà, hài hòa giữa vị ngọt đầu lưỡi và vị đắng thanh thoát nơi hậu vị.',N'ca_phe_phin_nau_da.png',null, 1),
		   (N'Cold Brew Truyền Thống',45000.0000,N'Tại The Coffee House, Cold Brew được ủ và phục vụ mỗi ngày từ 100% hạt Arabica Cầu Đất với hương gỗ thông, hạt dẻ, nốt sô-cô-la đặc trưng, thoang thoảng hương khói nhẹ giúp Cold Brew giữ nguyên vị tươi mới.',N'cold_brew_truyen_thong.png',null, 1),
		   (N'Cold Brew Kim Quất',49000.0000,N'Vị chua ngọt của Kim Quất làm dậy lên hương vị trái cây tự nhiên vốn sẵn có trong hạt cà phê Arabica Cầu Đất, mang đến một ly Cold Brew vừa nhẹ nhàng và thanh mát, đã khát ngày hè.',N'cold_brew_kim_quat.png',null, 1),

		   (N'Matcha Latte Tây Bắc',45000.0000,N'Best seller của Nhà - rất phù hợp cho ai muốn nhập môn matcha. *Khuấy đều để thưởng trọn hương vị.',N'matcha_latte_tay_bac_da.png',null, 2),
		   (N'Matcha Latte Tây Bắc (Nóng)',49000.0000,N'Trà Xanh Latte (Nóng) là phiên bản rõ vị trà nhất. Nhấp một ngụm, bạn chạm ngay vị trà xanh Tây Bắc chát nhẹ hoà cùng sữa nguyên kem thơm béo, đọng lại hậu vị ngọt ngào, êm dịu. Không chỉ là thức uống tốt cho sức khoẻ, Trà Xanh Latte (Nóng) còn là cái ôm ấm áp của đồng bào Tây Bắc gửi cho người miền xuôi.',N'matcha_latte_tay_bac_nong.png',null, 2),
		   (N'Matcha Latte Tây Bắc Sữa Yến Mạch',55000.0000,N'Vị matcha êm mượt nhờ kết hợp với sữa yến mạch. *Khuấy đều để thưởng trọn hương vị.',N'matcha_latte_tay_bac_yen_mach.png',null, 2),
		   (N'Matcha Latte',55000.0000,N'Matcha Nhật Bản hảo hạng kết hợp sữa tươi mịn màng, cân bằng vị umami thanh nhẹ và độ béo dịu, mang đến thức uống thơm ngon, đầy năng lượng.',N'matcha-latte.png',null, 2),
		   (N'Matcha Tây Bắc Trân Châu Hoàng Kim',49000.0000,N'Matcha kết hợp đường đen Okinawa, sữa béo nhẹ và trân châu hoàng kim mềm dẻo, tạo nên hương vị đậm đà, ngọt ngào nhưng vẫn thanh mát.',N'matcha_tay_bac_tran_chau.png',null, 2),

		   (N'Trà Đào Cam Sả - Nóng',59000.0000,N'Vị thanh ngọt của đào, vị chua dịu của Cam Vàng nguyên vỏ, vị chát của trà đen tươi được ủ mới mỗi 4 tiếng, cùng hương thơm nồng đặc trưng của sả chính là điểm sáng làm nên sức hấp dẫn của thức uống này.',N'tra_dao_cam_sa_nong.png',null, 3),
		   (N'Trà Đào Cam Sả - Đá',49000.0000,N'Vị thanh ngọt của đào, vị chua dịu của Cam Vàng nguyên vỏ, vị chát của trà đen tươi được ủ mới mỗi 4 tiếng, cùng hương thơm nồng đặc trưng của sả chính là điểm sáng làm nên sức hấp dẫn của thức uống này.',N'tra-dao-cam-sa.png',null, 3),
		   (N'Oolong Tứ Quý Sen (Nóng)',59000.0000,N'Nền trà oolong hảo hạng kết hợp cùng hạt sen tươi, bùi bùi thơm ngon. Trà hạt sen là thức uống thanh mát, nhẹ nhàng phù hợp cho cả buổi sáng và chiều tối.',N'oolong_tu_quy_sen_nong.png',null, 3),
		   (N'Oolong Tứ Quý Sen',49000.0000,N'Nền trà oolong hảo hạng kết hợp cùng hạt sen tươi, bùi bùi và lớp foam cheese béo ngậy. Trà hạt sen là thức uống thanh mát, nhẹ nhàng phù hợp cho cả buổi sáng và chiều tối.',N'oolong_tu_quy_sen_da.png',null, 3),
		   
		   (N'Trà Sữa Oolong Nướng Sương Sáo',55000.0000,N'Tận hưởng hương vị Oolong nướng đậm đà được Nhà rang kỹ càng, quyện cùng sữa thơm béo, càng thêm hấp dẫn với sương sáo thanh mát.',N'tra_sua_oolong_nuong_suong_sao.png',null, 4),
		   (N'Trà Sữa Oolong Tứ Quý Sương Sáo',55000.0000,N'Trà Oolong Tứ Quý ngạt ngào hoa cỏ mùa xuân, hòa quyện cùng sữa thơm mịn màng, sương sáo thanh mát. Đó là Lộc Yêu Yêu ngọt ngào mà Nhà gửi gắm đến bạn.',N'tra_sua_oolong_tu_quy_suong_sao.png',null, 4),
		   (N'Hồng Trà Sữa Nóng',55000.0000,N'Từng ngụm trà chuẩn gu ấm áp, đậm đà beo béo bởi lớp sữa tươi chân ái hoà quyện. Trà đen nguyên lá âm ấm dịu nhẹ, quyện cùng lớp sữa thơm béo khó lẫn - hương vị ấm áp chuẩn gu trà, cho từng ngụm nhẹ nhàng, ngọt dịu lưu luyến mãi nơi cuống họng.',N'hong_tra_sua_nong.png',null, 4),
		   (N'Hồng Trà Sữa Trân Châu',55000.0000,N'Thêm chút ngọt ngào cho ngày mới với hồng trà nguyên lá, sữa thơm ngậy được cân chỉnh với tỉ lệ hoàn hảo, cùng trân châu trắng dai giòn có sẵn để bạn tận hưởng từng ngụm trà sữa ngọt ngào thơm ngậy thiệt đã.',N'hong_tra_sua_tran_chau.png',null, 4),
		   (N'Trà Đen Macchiato',55000.0000,N'Trà đen được ủ mới mỗi ngày, giữ nguyên được vị chát mạnh mẽ đặc trưng của lá trà, phủ bên trên là lớp Macchiato "homemade" bồng bềnh quyến rũ vị phô mai mặn mặn mà béo béo.',N'tra_den_macchiato.png',null, 4),
		   (N'Trà Sữa Oolong BLao',39000.0000,N'Tận hưởng hương vị núi rừng mát lành lắng đọng trong từng ngụm Trà Sữa Oolong B’Lao của Nhà. Từng lá trà được Nhà chắt chiu từ B’Lao (Lâm Đồng), sao (rang) kỹ càng để giữ trọn vị Oolong đậm đà, quyện cùng sữa thơm béo, hấp dẫn.',N'tra_sua_oolong_blao.png',null, 4),
		   
		   (N'Butter Croissant Sữa Đặc',35000.0000,N'Bánh Butter Croissant bạn đã yêu, nay yêu không lối thoát khi được chấm cùng sữa đặc. Thơm bơ mịn sữa, ngọt ngào lòng nhau!',N'croissant-sua-dac.png',null, 5),
		   (N'Matcha Burnt Cheesecake',55000.0000,N'Hòa quyện giữa phô mai béo ngậy và trà xanh đắng nhẹ, thơm lừng, tinh tế khó cưỡng.',N'matcha-burnt-cheesecake.png',null, 5),
		   (N'Burnt Cheesecake',55000.0000,N'Phô mai béo mịn, thơm lừng với lớp vỏ cháy xém đặc trưng, tan chảy ngay đầu lưỡi.',N'burnt-cheesecake.png',null, 5),
		   (N'Mochi Kem Trà Sữa Trân Châu',19000.0000,N'Ngoài dẻo thơm, trong mát lạnh với kem vị trà sữa. Một cắn là say đắm, hai cắn là ngất ngây với trân châu giòn dai. *Bánh cần được bảo quản mát và dùng ngon nhất trong vòng 2 giờ sau khi nhận hàng.',N'mochi-tra-sua.png',null, 5),
		   (N'Mochi Kem Phúc Bồn Tử',19000.0000,N'Bao bọc bởi lớp vỏ Mochi dẻo thơm, bên trong là lớp kem lạnh cùng nhân phúc bồn tử ngọt ngào. Gọi 1 chiếc Mochi cho ngày thật tươi mát. Sản phẩm phải bảo quán mát và dùng ngon nhất trong 2h sau khi nhận hàng.',N'mochi-phuc-bon-tu.png',null, 5),
		   (N'Mochi Kem Chocolate',19000.0000,N'Bao bọc bởi lớp vỏ Mochi dẻo thơm, bên trong là lớp kem lạnh cùng nhân chocolate độc đáo. Gọi 1 chiếc Mochi cho ngày thật tươi mát. Sản phẩm phải bảo quán mát và dùng ngon nhất trong 2h sau khi nhận hàng.',N'mochi-choco.png',null, 5),
		   (N'Mousse Tiramisu',35000.0000,N'Hương vị dễ ghiền được tạo nên bởi chút đắng nhẹ của cà phê, lớp kem trứng béo ngọt dịu hấp dẫn',N'tiramisu.png',null, 5),

		   (N'Bánh Mì Que Chà Bông Phô Mai Bơ Cay',22000.0000,N'Aiiiii Bánh Mì Chà Bông Phô Mai hônggg? Chà bông tơi mịn đẫm phô mai Mozzarella kéo sợi, cay hít hà. Rộp rộp vỏ bánh giòn rụm nóng hổi, thêm ngấu nghiến với xốt bơ đậm đà.',N'bmq-cha-bong-pm.png',null, 6),
		   (N'Bánh Mì Que Pate Cột Đèn',19000.0000,N'Aiiiii Pate Cột Đèn đậm đà thơm béo hônggg? Rộp rộp vỏ bánh nóng hổi giòn rụm, thêm ngấu nghiến với pate cùng xốt bơ trứng đẫm vị.',N'bmq-pate-hai-phong.png',null, 6),
		   (N'Croissant trứng muối',39000.0000,N'Croissant trứng muối thơm lừng, bên ngoài vỏ bánh giòn hấp dẫn bên trong trứng muối vị ngon khó cưỡng.',N'croissant-trung-muoi.png',null, 6),
		   (N'Chà Bông Phô Mai',39000.0000,N'Chiếc bánh với lớp phô mai vàng sánh mịn bên trong, được bọc ngoài lớp vỏ xốp mềm thơm lừng. Thêm lớp chà bông mằn mặn hấp dẫn bên trên.',N'bami-cha-bong-pho-mai.png',null, 6)
		   

GO

select * from TbProducts


INSERT INTO [dbo].[TbNews]([Title],[PostedDate],[Content],[Image]) VALUES
           (N'UỐNG GÌ KHI TỚI SIGNATURE BY THE COFFEE HOUSE?',
			CAST(N'2025-02-09' AS Date),
			N'Các đại diện của SIGNATURE by The Coffee House đã có những gợi ý khá hấp dẫn dành cho các tín đồ mê cà phê. Theo đó, anh Đinh Anh Huân – Chủ tịch tập đoàn Seedcom, khuyên mọi người khi đến đây hãy thử Fine Robusta mà theo anh là “niềm tự hào” không chỉ của cà phê Việt Nam nói chung mà còn của The Coffee House nói riêng.
			Trong khi đó, anh Thanh Hòa – Trưởng nhóm nghiên cứu sản phẩm của The Coffee House, thì có 2 lựa chọn. Thứ nhất dành cho các quý cô không uống được cà phê thì một ly Latte đá kèm 1 shot Espresso rất đáng tham khảo. Riêng với những ai “ghiền” cà phê thì 1 set gồm 3 hương vị mới của SIGNATURE tại quầy sẽ mang đến trải nghiệm mới mẻ.
			Riêng anh Edward Teonadi – Giám đốc phát triển sản phẩm của The Coffee House , thì luôn trung thành với Flat White. Cuối cùng, anh Ngô Nguyên Kha – CEO của The Coffee House thì tin rằng với một không gian đầy cảm hứng như thế này thì “hãy nuông chiều bản thân bằng một ly Affogato thêm một shot Nut Cracker Blend”.
			Đến với SIGNATURE by The Coffee House bạn không chỉ tìm được hương vị cà phê của riêng mình, mà hơn cả đó là một trải nghiệm vô cùng độc đáo, làm nên cuộc hẹn tròn đầy.',
			N'uong_gi_khi_toi_signature.jpg'),

			(N'BÍ QUYẾT PHA CÀ PHÊ ROBUSTA CHUẨN VỊ TẠI NHÀ',
			 CAST('2025-05-12' AS DATE),
			N'Robusta luôn được xem là “linh hồn” của cà phê Việt với hương vị mạnh mẽ và đậm đà. Không ít người yêu cà phê luôn mong muốn tự pha cho mình một tách Robusta chuẩn vị ngay tại nhà mà không cần đến quán.
			Để có một ly cà phê thơm trọn vẹn, bạn hãy chọn loại hạt Robusta rang vừa, giữ được dầu thơm tự nhiên. Khi pha, hãy đảm bảo nước sôi từ 92–96°C để chiết xuất đúng lượng caffeine và hương vị nguyên bản.
			Chỉ với vài thao tác đơn giản, bạn hoàn toàn có thể tạo ra ly cà phê Robusta “đỉnh của chóp” như được pha bởi barista chuyên nghiệp.',
			N'bi_quyet_pha_robusta.jpg'),

			(N'TRẢI NGHIỆM KHÔNG GIAN MỚI TẠI THE COFFEE HOUSE',
			 CAST('2025-03-14' AS DATE),
			N'The Coffee House vừa ra mắt không gian mới trẻ trung, hiện đại, mang cảm giác thư thái nhưng đầy cảm hứng. Không chỉ là nơi thưởng thức cà phê, đây còn là điểm hẹn lý tưởng cho những buổi làm việc, gặp gỡ bạn bè hay đơn giản là tìm nơi “chill” sau một ngày dài.
			Gam màu gỗ ấm áp, ánh sáng dịu nhẹ và mùi cà phê thoang thoảng trong không khí sẽ khiến bạn như được “xạc pin” lại năng lượng.
			Nếu muốn tìm một nơi vừa nhâm nhi cà phê vừa thả lỏng tâm trí, The Coffee House phiên bản mới chắc chắn là lựa chọn hoàn hảo.',
			N'khong_gian_moi_tch.jpg'),

			(N'RA MẮT DÒNG SẢN PHẨM COLD BREW MỚI MẺ',
			 CAST('2025-08-15' AS DATE),
			N'The Coffee House tiếp tục mang đến sự đổi mới với dòng Cold Brew ủ lạnh suốt 16 giờ, giữ trọn sự tươi mát và thanh khiết của hạt cà phê nguyên bản. Hương vị nhẹ nhàng, ít đắng nhưng dư vị kéo dài phù hợp cho những ai thích gu cà phê thanh thoát.
			Cold Brew được kết hợp với cam vàng, sữa tươi hoặc kem béo để tạo ra nhiều phiên bản hợp gu giới trẻ.
			Một thức uống lý tưởng cho những ngày nóng hoặc dành cho người thích cà phê nhưng không chuộng vị đắng gắt.',
			N'coldbrew_ra_mat.jpg'),

			(N'HÀNH TRÌNH KHÁM PHÁ HẠT ARABICA VIỆT NAM',
			 CAST('2025-07-17' AS DATE),
			N'Arabica Việt Nam đang dần khẳng định vị thế trên bản đồ cà phê thế giới nhờ hương thơm thanh thoát và vị chua dịu đầy quyến rũ. Những vùng đất như Cầu Đất – Lâm Đồng đã tạo nên nhiều lô Arabica chất lượng cao chuẩn quốc tế.
			The Coffee House mang sứ mệnh đưa hạt Arabica Việt Nam đến gần hơn với người thưởng thức bằng việc chọn lọc từng mẻ rang, giữ nguyên sự cân bằng giữa hương trái cây và vị ngọt hậu.
			Mỗi tách Arabica không chỉ là cà phê, mà còn là câu chuyện về vùng đất, khí hậu và bàn tay cần mẫn của người nông dân Việt.',
			N'hanh_trinh_arabica.jpg'),

			(N'BỘ SƯU TẬP BÁNH MỚI DÀNH CHO MÙA LỄ',
			 CAST('2025-02-20' AS DATE),
			N'The Coffee House giới thiệu bộ sưu tập bánh ngọt mới với 3 hương vị: Mousse Cacao, Cheese Berry và Caramel Cream. Mỗi món được chế biến tỉ mỉ, kết hợp giữa vị béo ngậy, chua nhẹ của trái cây và độ mềm mịn vừa phải.
			Bộ sưu tập được thiết kế để kết hợp hoàn hảo với cà phê và trà của Nhà, đặc biệt là cappuccino, latte hay oolong thanh mát.
			Đây chắc chắn là sự lựa chọn tuyệt vời cho buổi hẹn hò, gặp gỡ bạn bè hoặc đơn giản là tự thưởng cho bản thân trong những ngày bận rộn.',
			N'bo_suu_tap_banh_moi.png'),

			(N'CÀ PHÊ PHIN – NÉT VĂN HOÁ VIỆT KHÔNG THỂ THAY ĐỔI',
			 CAST('2025-02-22' AS DATE),
			N'Cà phê phin đã trở thành một phần không thể thiếu trong văn hoá Việt Nam. Không chỉ là thức uống, phin cà phê còn là khoảnh khắc chậm lại để thưởng thức cuộc sống.
			Tại The Coffee House, cà phê phin được chọn lọc từ những hạt Robusta chất lượng cao, rang theo phong cách truyền thống nhưng vẫn đảm bảo sự tinh tế trong hương vị.
			Một tách phin sánh đậm, thơm nồng chính là sự kết hợp hoàn hảo giữa truyền thống và hiện đại, dành cho những ai yêu sự sâu sắc và nguyên bản.',
			N'ca_phe_phin_viet.jpg'),

			(N'KHÁM PHÁ HƯƠNG VỊ TRÀ SỮA OOLONG MỚI',
			 CAST('2025-04-25' AS DATE),
			N'Trà sữa Oolong phiên bản mới của The Coffee House mang đến hương vị trẻ trung với lớp kem sữa béo nhẹ và hương Oolong đậm đà.
			Lá trà được ủ tươi hằng ngày, kết hợp cùng sữa tươi thanh nhẹ tạo ra sự hài hoà hoàn hảo trong từng ngụm.
			Dành cho những ai yêu trà nhưng vẫn muốn cảm nhận sự béo mềm đặc trưng của trà sữa.',
			N'tra_sua_oolong_moi.jpg'),

			(N'THE COFFEE HOUSE GIỚI THIỆU DÒNG SỮA TƯƠI CÀ PHÊ MỚI',
			 CAST('2025-02-23' AS DATE),
			N'Sữa tươi cà phê là sự kết hợp hoàn hảo giữa hương vị sữa mềm mịn và vị cà phê đậm chất Việt. Phiên bản mới của The Coffee House mang đến sự cân bằng hài hoà giữa vị ngọt nhẹ, béo dịu và cà phê thơm lừng.
			Đây là lựa chọn tuyệt vời cho những ai không thích cà phê quá mạnh nhưng vẫn muốn giữ sự tỉnh táo suốt cả ngày.
			Một ly sữa tươi cà phê lạnh sẽ giúp bạn hứng khởi trong từng khoảnh khắc.',
			N'sua_tuoi_ca_phe_moi.png');

			
GO

INSERT INTO [dbo].[TbFeedbacks]
           ([Title]
           ,[PhoneNumber]
           ,[Content])
     VALUES
			(N'Chất lượng tốt', '0911111111', N'Đồ uống ổn, nhân viên thân thiện'),
			(N'Phục vụ nhanh', '0911222333', N'Gọi món chưa đến 5 phút đã có'),
			(N'Không gian đẹp', '0933444555', N'Không gian yên tĩnh, phù hợp học tập'),
			(N'Cần thêm món mới', '0977666888', N'Menu hơi ít, nên bổ sung món theo mùa'),
			(N'Giá hơi cao', '0988777666', N'Giá cao hơn so với quán gần nhà'),
			(N'Âm nhạc hay', '0909123456', N'Playlist chill, rất hợp buổi tối'),
			(N'Bàn hơi nhỏ', '0933555777', N'Đi nhóm đông hơi khó sắp xếp'),
			(N'Phục vụ chưa tốt', '0911555777', N'Đợi hơi lâu khi khách đông'),
			(N'Rất hài lòng', '0922113344', N'Sẽ quay lại nhiều lần nữa'),
			(N'Cà phê thơm', '0966888777', N'Hương thơm rất dễ chịu');
GO

INSERT INTO [dbo].[TbCustomers]
           ([Id]
           ,[Name]
           ,[PhoneNumber]
           ,[Address])
     VALUES
			 ('FA49B72B-0CEB-4569-BB41-08B72D7F16AA', N'Nguyễn Văn An', '0901111222', N'Hà Nội'),
			('5A28EC45-0625-4FDF-BC06-92594D0CF12F', N'Trần Thị Bình', '0912333444', N'Hải Phòng'),
			('884C5363-771D-4F67-BE93-B98FF6EC1329', N'Lê Minh Chương', '0923555666', N'Đà Nẵng'),
			('AA8E991B-B0F5-4377-A954-659C71091151', N'Phạm Hữu Dư', '0934666777', N'TP.HCM'),
			('506D022A-F082-47D4-9FC7-DBBE15379C79', N'Võ Thị Êm', '0945777888', N'Cần Thơ'),
			('71D3774F-8586-478F-816D-B28F0AF1B7EE', N'Đặng Văn Ngữ', '0956888999', N'Hà Nam'),
			('9C4E1A12-DF23-4C72-AE07-7DEC01EDCBED', N'Ngô Gia Bảo', '0967999000', N'Nam Định'),
			('B38F2F67-D64A-4357-92F4-326560A0F570', N'Hoàng Bảo Hùng', '0978111222', N'Bắc Ninh'),
			('A97D3CA5-E9D1-44F5-96AC-3A577B5951B2', N'Bùi Khánh Duy', '0989222333', N'Hòa Bình'),
			('06257231-D68E-413C-9FD0-BDC0E724E023', N'Đỗ Hải Khải', '0990333444', N'Thanh Hóa');
GO

INSERT INTO [dbo].[TbBills]
           ([Id]
           ,[OrderNumber]
           ,[OrderDate]
           ,[TotalAmount]
           ,[CustomerId])
     VALUES
			('D6B69838-A243-4BB6-8A85-60F62ABD6314', 'ORD001', '2025-01-01', 95000,  'FA49B72B-0CEB-4569-BB41-08B72D7F16AA'),
			('67B68899-2F6E-4986-A500-B894C4BB834E', 'ORD002', '2025-01-02', 120000, '5A28EC45-0625-4FDF-BC06-92594D0CF12F'),
			('3D771025-6D14-4A9F-A705-9B8DD238B378', 'ORD003', '2025-01-03', 45000, '884C5363-771D-4F67-BE93-B98FF6EC1329'),
			('9B17F3B0-F415-48C8-A906-AAF16ABAAE85', 'ORD004', '2025-01-04', 200000,'AA8E991B-B0F5-4377-A954-659C71091151'),
			('4C322958-41B5-4631-8F30-9D41D6A390D1', 'ORD005', '2025-01-05', 175000, '506D022A-F082-47D4-9FC7-DBBE15379C79'),
			('B645C27B-23D3-4A65-9683-17BCC29CD092', 'ORD006', '2025-01-06', 85000,  '71D3774F-8586-478F-816D-B28F0AF1B7EE'),
			('89F89BE1-C555-4ABE-83A5-D661EF4F27E8', 'ORD007', '2025-01-07', 145000,'9C4E1A12-DF23-4C72-AE07-7DEC01EDCBED'),
			('29F1807A-A782-4B07-AF81-320DE6CA0C40', 'ORD008', '2025-01-08', 300000,'B38F2F67-D64A-4357-92F4-326560A0F570'),
			('544CE90D-26C0-4E97-90A9-DDDB525EF383', 'ORD009', '2025-01-09', 110000, 'A97D3CA5-E9D1-44F5-96AC-3A577B5951B2'),
			('6ADE541A-E38C-4BC5-9F94-0F3CF8F44F20', 'ORD010', '2025-01-10', 59000, '06257231-D68E-413C-9FD0-BDC0E724E023');
GO

INSERT INTO [dbo].[TbBillDetails]
           ([BillId]
           ,[ProductId]
           ,[UnitPrice]
           ,[Quantity]
           ,[Discount]
           ,[Total])
     VALUES
			('D6B69838-A243-4BB6-8A85-60F62ABD6314',  1, 25000, 2, 0,      50000),
			('67B68899-2F6E-4986-A500-B894C4BB834E',  3, 40000, 3, 0,     120000),
			('3D771025-6D14-4A9F-A705-9B8DD238B378',  2, 45000, 1, 0,      45000),
			('4C322958-41B5-4631-8F30-9D41D6A390D1',  5, 50000, 4, 0,     200000),
			('4C322958-41B5-4631-8F30-9D41D6A390D1',  1, 35000, 5, 0,     175000),
			('B645C27B-23D3-4A65-9683-17BCC29CD092',  4, 85000, 1, 0,      85000),
			('89F89BE1-C555-4ABE-83A5-D661EF4F27E8',  2, 29000, 5, 0,     145000),
			('29F1807A-A782-4B07-AF81-320DE6CA0C40',  6, 60000, 5, 0,     300000),
			('544CE90D-26C0-4E97-90A9-DDDB525EF383',  3, 55000, 2, 0,     110000),
			('6ADE541A-E38C-4BC5-9F94-0F3CF8F44F20', 1, 59000, 1, 0,      59000);
GO


INSERT INTO [dbo].[TbAccounts]
           ([Id]
           ,[UserName]
           ,[Password])
     VALUES
           ('DBE837D2-BE98-4969-9E83-DCA1083EAA40','admin',
           'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')
GO