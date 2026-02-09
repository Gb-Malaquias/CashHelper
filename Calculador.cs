public class CalculadorGeral
{
    private readonly List<Gasto> _gastos = new();
    private int _nextId = 1;

    public Gasto Adicionar(TipoGasto categoria, string? descricao, decimal valor)
    {
        var gasto = new Gasto(categoria, descricao, valor);
        gasto.DefinirId(_nextId++);

        _gastos.Add(gasto);
        return gasto;
    }

    public Gasto Remover(int id)
    {
        var gasto = _gastos.FirstOrDefault(g => g.Id == id);

        if (gasto == null)
            throw new InvalidOperationException("Gasto não encontrado.");

        _gastos.Remove(gasto);
        return gasto;
    }

    public IReadOnlyList<Gasto> Listar()
        => _gastos.AsReadOnly();

    public decimal Total()
        => _gastos.Sum(g => g.Valor);

    public List<Gasto> ListarPorMes(int mes)
    {
        var anoAtual = DateTime.Now.Year;


        return _gastos
            .Where(g => g.Data.Month == mes && g.Data.Year == anoAtual)
            .ToList();
    }





}
