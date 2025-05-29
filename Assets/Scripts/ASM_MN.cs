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

    public string calculate_rank(int score)
    {
        // sinh vien viet tiep code o day
        if (score < 100) return "Hang dong";
        else if (score < 500) return "Bac";
        else if (score < 1000) return "Vang";
        else return "Kim cuong";
    }

    public Players YC1(string playerName, int score, int regionID)
    {
        // Tính rank dựa vào điểm
        string rank = calculate_rank(score);

        // Tạo đối tượng người chơi mới
        Players newPlayer = new Players(playerName, score, regionID, rank);

        // Lưu người chơi vào danh sách
        listPlayer.Add(newPlayer);

        Debug.Log("Da luu thong tin nguoi choi: " + playerName + ", diem: " + score);

        // Trả về người chơi vừa tạo
        return newPlayer;
    }

    public void YC2()
    {
        // sinh vien viet tiep code o day
        Debug.Log("====== DANH SACH NGUOI CHOI ======");

        for (int i = 0; i < listPlayer.Count; i++)
        {
            Players player = listPlayer[i];
            Region region = listRegion.FirstOrDefault(r => r.ID == player.RegionID);
            string regionName = region != null ? region.Name : "Khong xac dinh";

            Debug.Log($"ID: {i + 1}, Ten: {player.Name}, Diem: {player.Score}, Khu vuc: {regionName}, Rank: {player.Rank}");
        }
    }

    public void YC3()
    {
        // sinh vien viet tiep code o day
        if (listPlayer.Count == 0) return;

        Players currentPlayer = listPlayer.Last();
        int currentScore = currentPlayer.Score;

        Debug.Log($"Cac nguoi choi co diem thap hon {currentPlayer.Name} ({currentScore}):");

        foreach (Players p in listPlayer)
        {
            if (p != currentPlayer && p.Score < currentScore)
            {
                Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
                string regionName = region != null ? region.Name : "Khong xac dinh";

                Debug.Log($"Ten: {p.Name}, Diem: {p.Score}, Khu vuc: {regionName}, Rank: {p.Rank}");
            }
        }
    }

    public void YC4()
    {
        // sinh vien viet tiep code o day
        if (listPlayer.Count == 0) return;

        int currentIndex = listPlayer.Count - 1;
        var player = listPlayer.ElementAtOrDefault(currentIndex);

        if (player != null)
        {
            Region region = listRegion.FirstOrDefault(r => r.ID == player.RegionID);
            string regionName = region != null ? region.Name : "Khong xac dinh";

            Debug.Log($"Thong tin nguoi choi ID {currentIndex + 1}:");
            Debug.Log($"Ten: {player.Name}, Diem: {player.Score}, Khu vuc: {regionName}, Rank: {player.Rank}");
        }
    }

    public void YC5()
    {
        // sinh vien viet tiep code o day
        var sorted = listPlayer.OrderByDescending(p => p.Score).ToList();

        Debug.Log("Danh sach nguoi choi theo diem giam dan:");
        foreach (var p in sorted)
        {
            Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
            string regionName = region != null ? region.Name : "Khong xac dinh";

            Debug.Log($"Ten: {p.Name}, Diem: {p.Score}, Khu vuc: {regionName}, Rank: {p.Rank}");
        }
    }

    public void YC6()
    {
        // sinh vien viet tiep code o day
        var bottom5 = listPlayer.OrderBy(p => p.Score).Take(5).ToList();

        Debug.Log("5 nguoi choi co diem thap nhat:");
        foreach (var p in bottom5)
        {
            Region region = listRegion.FirstOrDefault(r => r.ID == p.RegionID);
            string regionName = region != null ? region.Name : "Khong xac dinh";

            Debug.Log($"Ten: {p.Name}, Diem: {p.Score}, Khu vuc: {regionName}, Rank: {p.Rank}");
        }
    }

    public void YC7()
    {
        // sinh vien viet tiep code o day
    }

    void CalculateAndSaveAverageScoreByRegion()
    {
        // sinh vien viet tiep code o day
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
    // sinh vien viet tiep code o day
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
