using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class RelatorioCategoria : ContentPage
{
    public RelatorioCategoria()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        try
        {
            List<Produto> todos = await App.Db.GetAll();

            List<CategoriaTotal> relatorio = todos
                .GroupBy(p => string.IsNullOrWhiteSpace(p.Categoria) ? "Sem Categoria" : p.Categoria)
                .Select(g => new CategoriaTotal
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.Total)
                })
                .OrderByDescending(g => g.Total)
                .ToList();

            lst_relatorio.ItemsSource = relatorio;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}