using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MauiApptt.Data;

public static class StocksService
{
    private static void SaveAll(Guid userId, List<StockItem> stocks)
    {
        string appDataDirectoryPath = Utils.GetAppDirectoryPath();
        string stocksFilePath = Utils.GetTodosFilePath(userId);

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(stocks);
        File.WriteAllText(stocksFilePath, json);
    }

    public static List<StockItem> GetAll(Guid userId)
    {
        string stocksFilePath = Utils.GetTodosFilePath(userId);
        if (!File.Exists(stocksFilePath))
        {
            return new List<StockItem>();
        }

        var json = File.ReadAllText(stocksFilePath);

        return JsonSerializer.Deserialize<List<StockItem>>(json);
    }

    public static List<StockItem> Create(Guid userId, string itemName, int quantity )
    {
       

        List<StockItem> stocks = GetAll(userId);
        stocks.Add(new StockItem
        {
            ItemName = itemName,
            CreatedBy = userId,
            Quantity = quantity
        });
        SaveAll(userId, stocks);
        return stocks;
    }

    public static List<StockItem> Delete(Guid userId, Guid id)
    {
        List<StockItem> stocks = GetAll(userId);
        StockItem stock = stocks.FirstOrDefault(x => x.Id == id);

        if (stock == null)
        {
            throw new Exception("Item not found.");
        }

        stocks.Remove(stock);
        SaveAll(userId, stocks);
        return stocks;
    }

    public static void DeleteByUserId(Guid userId)
    {
        string stocksFilePath = Utils.GetTodosFilePath(userId);
        if (File.Exists(stocksFilePath))
        {
            File.Delete(stocksFilePath);
        }
    }

    public static List<StockItem> Update(Guid userId, Guid id, string itemName, int quantity)
    {
        List<StockItem> stocks = GetAll(userId);
        StockItem stockToUpdate = stocks.FirstOrDefault(x => x.Id == id);

        if (stockToUpdate == null)
        {
            throw new Exception("Item not found.");
        }

        stockToUpdate.ItemName = itemName;
        stockToUpdate.Quantity = quantity;
        SaveAll(userId, stocks);
        return stocks;
    }
}


