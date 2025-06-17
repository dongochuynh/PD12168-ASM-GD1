using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    // Tính rank dựa trên score
    public string calculate_rank(int score)
    {
        if (score < 100) return "Hạng đồng";
        else if (score < 500) return "Bạc";
        else if (score < 1000) return "Vàng";
        else return "Kim cương";
    }

    // YC1: Lưu thông tin người chơi
    public Players YC1(string playerName, int score, int regionID)
    {
        string rank = calculate_rank(score);
        Players newPlayer = new Players(playerName, score, regionID, rank);
        listPlayer.Add(newPlayer);

        Debug.Log("Đã lưu thông tin người chơi: " + playerName + ", điểm: " + score);

        // Xuất danh sách người chơi đã lưu
        Debug.Log("====== DANH SÁCH NGƯỜI CHƠI ĐÃ LƯU ======");
        for (int i = 0; i < listPlayer.Count; i++)
        {
            Players p = listPlayer[i];
            Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
            string regionName = region != null ? region.Name : "Không xác định";
            Debug.Log($"ID: {i + 1}, Tên: {p.Name}, Điểm: {p.Score}, Khu vực: {regionName}, Rank: {p.Rank}");
        }

        return newPlayer;
    }

    // YC2: In ra tất cả người chơi
    public void YC2()
    {
        Debug.Log("====== DANH SÁCH NGƯỜI CHƠI ======");
        for (int i = 0; i < listPlayer.Count; i++)
        {
            Players p = listPlayer[i];
            Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
            string regionName = region != null ? region.Name : "Không xác định";
            Debug.Log($"ID: {i + 1}, Tên: {p.Name}, Điểm: {p.Score}, Khu vực: {regionName}, Rank: {p.Rank}");
        }
    }

    // YC3: In ra các player có score bé hơn score hiện tại
    public void YC3()
    {
        if (listPlayer.Count == 0) return;
        Players currentPlayer = listPlayer.Last();
        int currentScore = currentPlayer.Score;

        Debug.Log($"Các người chơi có điểm thấp hơn {currentPlayer.Name} ({currentScore}):");
        foreach (Players p in listPlayer)
        {
            if (p != currentPlayer && p.Score < currentScore)
            {
                Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
                string regionName = region != null ? region.Name : "Không xác định";
                Debug.Log($"Tên: {p.Name}, Điểm: {p.Score}, Khu vực: {regionName}, Rank: {p.Rank}");
            }
        }
    }

    // YC4: Tìm player theo Id hiện tại (dùng Linq)
    public void YC4()
    {
        if (listPlayer.Count == 0) return;
        int currentIndex = listPlayer.Count - 1;
        var player = listPlayer.ElementAtOrDefault(currentIndex);

        if (player != null)
        {
            Region region = listRegion.FirstOrDefault(r => r.ID == player.RegionID);
            string regionName = region != null ? region.Name : "Không xác định";
            Debug.Log($"Thông tin người chơi ID {currentIndex + 1}:");
            Debug.Log($"Tên: {player.Name}, Điểm: {player.Score}, Khu vực: {regionName}, Rank: {player.Rank}");
        }
    }

    // YC5: Xuất thông tin 5 Player có score cao nhất theo thứ tự giảm dần
    public void YC5()
    {
        var top5 = listPlayer.OrderByDescending(p => p.Score).Take(5).ToList();
        Debug.Log("Top 5 người chơi có điểm cao nhất:");
        for (int i = 0; i < top5.Count; i++)
        {
            var p = top5[i];
            Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
            string regionName = region != null ? region.Name : "Không xác định";
            Debug.Log($"Top {i + 1}: Tên: {p.Name}, Điểm: {p.Score}, Khu vực: {regionName}, Rank: {p.Rank}");
        }
    }

    // YC6: Xuất 5 player có score thấp nhất theo thứ tự tăng dần
    public void YC6()
    {
        var bottom5 = listPlayer.OrderBy(p => p.Score).Take(5).ToList();
        Debug.Log("5 người chơi có điểm thấp nhất:");
        foreach (var p in bottom5)
        {
            Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
            string regionName = region != null ? region.Name : "Không xác định";
            Debug.Log($"Tên: {p.Name}, Điểm: {p.Score}, Khu vực: {regionName}, Rank: {p.Rank}");
        }
    }

    // YC7: Tạo thread tính score trung bình theo region và lưu vào file
    public void YC7()
    {
        // Lấy path ở main thread
        string filePath = Path.Combine(Application.persistentDataPath, "bxhRegion.txt");

        Thread thread = new Thread(() =>
        {
            List<string> lines = new List<string>();

            foreach (var region in listRegion)
            {
                var playersInRegion = listPlayer.Where(p => p.RegionID == region.ID).ToList();
                double avgScore = playersInRegion.Count > 0
                    ? playersInRegion.Average(p => p.Score)
                    : 0;

                string line = $"Khu vực: {region.Name} | Điểm trung bình: {avgScore:F2}";
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
            Debug.Log($"Đã lưu điểm trung bình theo khu vực vào: {filePath}");
        });
        thread.Name = "BXH";
        thread.Start();
    }
}

[SerializeField]    
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public string Name;
    public int Score;
    public int RegionID;
    public string Rank;

    public Players(string name, int score, int regionID, string rank)
    {
        this.Name = name;
        this.Score = score;
        this.RegionID = regionID;
        this.Rank = rank;
    }
}
