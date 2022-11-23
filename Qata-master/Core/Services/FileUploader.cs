using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public class FileUploader
    {

       
        public async Task<string> UploadFile(IFormFile file)
        {
            FileStream stream;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + Guid.NewGuid().ToString());
            if (!Directory.Exists(path))
            {
                DirectoryInfo dir = Directory.CreateDirectory(path);
            }
            string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            filename = EnsureCorrectFilename(filename);

            //if (new string[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(file.FileName)))
            //{
            //}


                using (FileStream output = System.IO.File.Create(path + "/" + filename))
                {
                    await file.CopyToAsync(output);


                }
            
            stream = new FileStream(Path.Combine(path + "/" + filename), FileMode.Open);




          //  System.IO.Directory.Delete(path, true);
            return "";
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        //public async Task<object> imageupload(IFormFile avatar, int id)
        //{

        //    string directory = "";


        //    string dirpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/urunimg/" + directory + id.ToString());
        //    if (!Directory.Exists(dirpath))
        //    {
        //        DirectoryInfo dir = Directory.CreateDirectory(dirpath);
        //    }



        //    string filename = ContentDispositionHeaderValue.Parse(avatar.ContentDisposition).FileName.Trim('"');

        //    filename = EnsureCorrectFilename(filename);

        //    if (new string[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(avatar.FileName)))
        //    {
        //        //var tt = this.GetPathAndFilename("/" + filename);

        //        using (FileStream output = System.IO.File.Create(dirpath + "/" + filename))
        //        {
        //            await avatar.CopyToAsync(output);
        //            ///resultobj.status = true;
        //            //createEducationDirectory(source.FileName);

        //        }
        //    }


        //    return "";
        //}





    }
}
