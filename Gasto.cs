public enum TipoGasto
{
    Alimentacao = 1,
    Moradia = 2,
    Transporte = 3,
    Lazer = 4,
    Saude = 5,
    Educacao = 6,
    Outros = 7
}

public class Gasto
{
    public int Id { get; private set; }
    public string? Descricao { get; private set; }
    public TipoGasto Categoria { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }

    public Gasto(TipoGasto categoria, string? descricao, decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do gasto deve ser maior que zero.");

        if (categoria == TipoGasto.Outros)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição é obrigatória para a categoria 'Outros'.");

            Descricao = descricao.Trim();
        }

        Categoria = categoria;
        Valor = valor;
        Data = DateTime.Now;
    }

    internal void DefinirId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID inválido.");

        Id = id;
    }
}
