using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using project_music.Models;
using System.Text;


public class NewDBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public NewDBservices()
    {
      
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

   
    // This method inserts a user to DB // 
    public int Insert(Users user)
    {

        SqlConnection con;
        SqlCommand cmd1;

        try
        {
            con = connect("ConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommand(user);      // helper method to build the insert string   
        cmd1 = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd1.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException ex)
        {
            // the exception alone won't tell you why it failed...
            if (ex.Number == 2627)
            {

                throw new Exception("The User Is Already Exists");
            }
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    // Build the Insert command Users Information //
    private String BuildInsertCommand(Users user)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}')", user.Name, user.FamilyName, user.Gender, user.Age.ToString(), user.Address, user.Email, user.MusicType, user.Password, user.Image );
        String prefix = "INSERT INTO Users " + "( Name, FamilyName,Gender,Age,Address, Email,MusicType,Password,Image) output INSERTED.UserId ";
        command = prefix + sb.ToString();

        return command;

    }


    /****************************** Get All User Tweets  ******************************************/
    public List<Tweet> GetTweetsFromDB(string constring, string tableName, int UsID)
    {
        string Uid = UsID.ToString();
        SqlConnection con = null;
        List<Tweet> Tw = new List<Tweet>();
        try
        {
            con = connect(constring); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT * FROM " + tableName + " where UserId = " + Uid;/*+" P inner join hobbieForUserNew H on P.id = H.id";*/

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Tweet T = new Tweet();
                T.UserId = Convert.ToInt32(dr["UserId"]);
                T.TweetID = Convert.ToInt32(dr["TweetID"]);
                T.Username = (string)dr["Username"];
                T.Text = (string)dr["Text"];
                T.Date = (string)dr["Date"];
                T.ProfileImageUrl = (string)dr["ProfileImageUrl"];
                T.Url = (string)dr["Url"];
                Tw.Add(T);
            }

            return Tw;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            { con.Close(); }
        }
    }
    //*********************************************Get Videos To PlayList*******************************************************************//
    public List<Video> GetYoutubesFromDB(string constring, string tableName, int UsID)
    {
        string Uid = UsID.ToString();
        SqlConnection con = null;
        List<Video> Vid = new List<Video>();
        try
        {
            con = connect(constring); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT * FROM " + tableName + " where UserId = " + Uid;

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Video V = new Video();
                V.UserId = Convert.ToInt32(dr["UserId"]);
                V.YoutubeID = Convert.ToInt32(dr["YoutubeID"]);
                V.VideoId = (string)dr["VideoId"];
                V.PublishedAt = (string)dr["PublishedAt"];
                V.Title = (string)dr["Title"];
                V.Description = (string)dr["Description"];
                Vid.Add(V);
            }

            return Vid;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            { con.Close(); }
        }
    }

    /******************************Delete Video From DataBase******************************************/
    public int RemoveYoutubeFromDB(string constring, string tablename, string VideoId)
    {
        SqlConnection con = connect(constring);
        SqlCommand cmd2;
        String cStr2 = "";
        cStr2 = "DELETE FROM " + tablename + " WHERE VideoId =" +"'" +VideoId+"'";
        cmd2 = CreateCommand(cStr2, con);
        int numAffected2;
        try
        {
            numAffected2 = cmd2.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return numAffected2;

    }

    /******************************Delete Tweets From DB******************************************/
    public int RemoveTweetFromDB(string constring, string tablename, int TweetID)
    {
        SqlConnection con = connect(constring);
        SqlCommand cmd2;
        String cStr2 = "";
        cStr2 = "DELETE FROM " + tablename + " WHERE TweetID =" + "'" + TweetID + "'";
        cmd2 = CreateCommand(cStr2, con);
        int numAffected2;
        try
        {
            numAffected2 = cmd2.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return numAffected2;

    }
  
    //**************************************User Login*******************************************//
    public Users Login(string userName, string password, string constring, string tableName)
    {
        Users user = new Users();
        SqlConnection con = null;
        try
        {

            con = connect(constring); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "SELECT * FROM " + tableName + " WHERE Email='" + userName + "'";
            SqlCommand cmd1 = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            if (dr.HasRows == false)
            {
                user.UserId = 0;
                return user;
            }
            else
            {
                con = connect(constring); // create a connection to the database using the connection String defined in the web config file
                String selectSTR2 = "SELECT * FROM " + tableName + " WHERE Email='" + userName + "' and Password = '" + password + "'";
                SqlCommand cmd2 = new SqlCommand(selectSTR2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                if (dr2.HasRows == false)
                {
                    user.UserId = 1;
                    return user;
                }
                else
                {
                    while (dr2.Read())
                    {   // Read till the end of the data into a row


                        user.UserId = Convert.ToInt32(dr2["UserId"]);
                        user.Name = (string)dr2["Name"];
                        user.FamilyName = (string)dr2["FamilyName"];
                        user.Gender = (string)dr2["Gender"];
                        user.Age = Convert.ToDouble(dr2["Age"]);
                        user.Address = (string)dr2["Address"];
                        user.Image = (string)dr2["image"];
                        user.Email = (string)dr2["email"];
                        user.Password = (string)dr2["password"];
                        user.MusicType = (string)dr2["MusicType"];

                    }

                    return user;
                }
            }
           
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }

    //*************************************************************************************
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    //***********************************Insert Tweets To DB***************************************************//
    public int InsertTweets(Tweet tw)
    {

        SqlConnection con;
        SqlCommand cmd1;

        try
        {
            con = connect("ConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandTweets(tw);      // helper method to build the insert string
        //String cstr2 = "";
        cmd1 = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd1.ExecuteNonQuery(); // execute the command
            return numEffected;

        }
        catch (SqlException ex)
        {
            // the exception alone won't tell you why it failed...
            if (ex.Number == 2627)
            {
                throw new Exception("The Tweet is already exists");
            }
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the tweet command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandTweets(Tweet tw)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}',{5})", tw.Username, tw.Text, tw.Date, tw.ProfileImageUrl, tw.Url, tw.UserId);
        String prefix = "INSERT INTO Tweets " + "(Username, Text,Date,ProfileImageUrl,Url,UserId) output INSERTED.TweetID ";
        command = prefix + sb.ToString();

        return command;

    }
    //******************************Insert videos to DB********************************************************//
    public int InsertYoutubes(Video vi)
    {

        SqlConnection con;
        SqlCommand cmd1;

        try
        {
            con = connect("ConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandYoutubes(vi);      // helper method to build the insert string
        cmd1 = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd1.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException ex)
        {
            // the exception alone won't tell you why it failed...
            if (ex.Number == 2627)
            {

                throw new Exception("The video is already exists");
            }
            return 0;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the video command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandYoutubes(Video v)
    {
        String command;
        string ReplacedDescriptionString = v.Description.Replace("'", "");
        string ReplacedTitleString = v.Title.Replace("'", "");


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}',{4})", v.VideoId, v.PublishedAt, ReplacedTitleString, ReplacedDescriptionString, v.UserId);
        String prefix = "INSERT INTO Youtubes " + "(VideoId, PublishedAt,Title,Description,UserId) output INSERTED.YoutubeID ";
        command = prefix + sb.ToString();

        return command;

    }

    /******************Updating User Data********************/
    public int SaveChange(Users user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
     
        String cStr = " UPDATE  Users " + "SET Name = '" + user.Name + "', FamilyName= '" + user.FamilyName + "', Gender= '" + user.Gender +
            "', Age= '" + user.Age +
            "', Address= '" + user.Address +
             "', Email= '" + user.Email +
              "', MusicType= '" + user.MusicType +
             "', Password= '" + user.Password +
            "', Image= '" + user.Image +

            "' where UserId='" + user.UserId+"'"; // helper method to build the insert string
   
           cmd = CreateCommand(cStr, con);             // create the command


        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
}