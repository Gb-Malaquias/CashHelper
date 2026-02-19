
public enum NivelGasto
{
    Ok,
    Atencao,
    Alerta
}

public record LimiteCategoria(decimal IdealMax, decimal AtencaoMax);

public static class PoliticaDeGastos
{
    public static readonly Dictionary<TipoGasto, LimiteCategoria> Limites =
        new()
        {
            { TipoGasto.Moradia,     new LimiteCategoria(0.30m, 0.35m) },
            { TipoGasto.Alimentacao, new LimiteCategoria(0.25m, 0.30m) },
            { TipoGasto.Transporte,  new LimiteCategoria(0.15m, 0.20m) },
            { TipoGasto.Lazer,       new LimiteCategoria(0.10m, 0.15m) },
            { TipoGasto.Saude,       new LimiteCategoria(0.10m, 0.15m) },
            { TipoGasto.Educacao,    new LimiteCategoria(0.10m, 0.15m) },
            { TipoGasto.Outros,      new LimiteCategoria(0.05m, 0.10m) }
        };
}


public static class AvaliadorDeGastos
{
    public static NivelGasto Avaliar(
        TipoGasto categoria,
        decimal totalCategoria,
        decimal salario)
    {
        if (salario <= 0)
            throw new ArgumentException("Salário inválido.");

        var percentual = totalCategoria / salario;

        var limite = PoliticaDeGastos.Limites[categoria];

        if (percentual <= limite.IdealMax)
            return NivelGasto.Ok;

        if (percentual <= limite.AtencaoMax)
            return NivelGasto.Atencao;

        return NivelGasto.Alerta;
    }
}


public static class GeradorDeMensagem
{
    public static string Gerar(
        TipoGasto categoria,
        decimal totalCategoria,
        decimal salario)
    {
        var percentual = totalCategoria / salario;
        var nivel = AvaliadorDeGastos.Avaliar(categoria, totalCategoria, salario);

        string percentualTexto = (percentual * 100).ToString("0.##");

        return nivel switch
        {
            NivelGasto.Ok =>
                $"Você gastou {percentualTexto}% do seu salário em {categoria}. Está dentro do normal.",

            NivelGasto.Atencao =>
                $"Você gastou {percentualTexto}% do seu salário em {categoria}. Atenção: esse valor passa um pouco do normal, Recomendo vigiar mais essa area.",

            NivelGasto.Alerta =>
                $"Você gastou {percentualTexto}% do seu salário em {categoria}. Alerta: gasto acima do normal. Considere reduzir despesas nessa área.",

            _ => "Não foi possível gerar recomendação."
        };
    }
}


public class Recomendador
{
    private readonly decimal _salario;
    private readonly IReadOnlyList<Gasto> _gastos;

    public Recomendador(decimal salario, IReadOnlyList<Gasto> gastos)
    {
        if (salario <= 0)
            throw new ArgumentException("Salário inválido.");

        _salario = salario;
        _gastos = gastos;
    }

    public IEnumerable<string> Avaliar()
    {
        var gastosPorCategoria = _gastos
            .GroupBy(g => g.Categoria);

        foreach (var grupo in gastosPorCategoria)
        {
            decimal totalCategoria = grupo.Sum(g => g.Valor);
            decimal percentual = totalCategoria / _salario;

            yield return GeradorDeMensagem.Gerar(
                grupo.Key,
                totalCategoria,
                _salario
            );
        }
    }
}

