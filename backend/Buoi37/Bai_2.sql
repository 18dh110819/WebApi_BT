--Câu 2.1
SELECT nv.TenNV, pb.TenPB, manager.TenNV as 'TenTruongPhong'
FROM NhanVien NV, PhongBan pb, NhanVien manager
WHERE nv.MaPB = pb.Id and nv.MaTruongPhong = manager.Id
ORDER BY pb.TenPB

--Câu 2.2
SELECT A.TenNV, B.TenDuAn, C.NgayThamGia
FROM NhanVien A, DuAn B, NhanVien_Duan C
WHERE A.Id = C.MaNhanVien AND B.Id = C.MaDuAn
ORDER BY A.TenNV

--Câu 2.3
SELECT A.TenDuAn, B.TenDiaDiem, C.MaNhanVien, C.NgayThamGia
FROM DuAn A, DiaDiem B, NhanVien_Duan C
WHERE A.Id = C.MaDuAn AND A.MaDiaDiem = B.Id