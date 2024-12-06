namespace eticketing.Models;

public record UserAccessTokenData(Guid Id, Roles Role);

public enum Roles
{
    // Enum nya bisa dikasih index secara langsung karena di database disimpan dalam tabel integernya
    // Menghindari urutan enum di kode terubah karena penambahan baru atau hal lainnya
    // Bisa terapkan untuk semua enum.
    // Enum file jangan diletakkan di folder ini karena campur sama model nya entity framework, bisa dibuatkan folder khusus enum
    Admin = 0,
    Customer = 1,
}
