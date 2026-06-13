--Câu 1.1
SELECT nv.TenNV, nv.SoDienThoai, pb.TenPB
FROM NhanVien nv, PhongBan pb
WHERE nv.MaPB = pb.Id 
ORDER BY PB.TenPB

--Câu 1.2
SELECT da.TenDuAn, da.NgayBatDau, da.NgayKetThuc, dd.TenDiaDiem, dd.DiaChi 
FROM DuAn da, DiaDiem dd 
WHERE da.MaDiaDiem = dd.Id
ORDER BY dd.Id

--Câu 1.3
SELECT DA.TenDuAn, NVDA.MaNhanVien, NVDA.NgayThamGia 
FROM NhanVien_Duan NVDA, DuAn DA
WHERE NVDA.MaNhanVien = NVDA.MaNhanVien AND DA.Id = NVDA.MaDuAn
ORDER BY NVDA.MaNhanVien