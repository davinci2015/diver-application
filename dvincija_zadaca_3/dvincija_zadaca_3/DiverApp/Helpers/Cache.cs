using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Helpers
{
    public class Cache
    {
        string[] cache;
        int cacheIndex;
        int cacheCapacity;
        int totalPages = 0;
        int limitToPrint;
        int pageOffset = 0;
        bool manualPaging = false;
        private static Cache instance;

        private Cache() { }

        public static Cache GetInstance()
        {
            if (instance == null)
                instance = new Cache();
            return instance;
        }

        public void InitCache(int cacheCapacity, int limit)
        {
            this.cacheCapacity = cacheCapacity;
            cache = new string[cacheCapacity];
            cacheIndex = 0;
            limitToPrint = limit;
            pageOffset = 0;
        }

        public void ClearCache()
        {
            cache = new string[cacheCapacity];
            cacheIndex = 0;
            pageOffset = 0;
        }

        public void SaveToCache(string text)
        {
            cache[cacheIndex++] = text;
            totalPages = cacheIndex == limitToPrint ? 0 : (int)Math.Floor((float) cacheIndex / limitToPrint);
            manualPaging = false;
        }

        public bool PageUp()
        {
            manualPaging = true;
            if (pageOffset < 0)
            {
                pageOffset += limitToPrint;
                return true;
            }

            return false;
        }

        public bool PageDown()
        {
            manualPaging = true;

            if (cacheIndex > limitToPrint && ( (cacheIndex + pageOffset - limitToPrint) > 0) )
            {
                pageOffset -= limitToPrint;
                return true;
            }

            return false;                
        }

        public string[] GetCache()
        {
            // If there is less 'rows' in cache than max rows
            if (cacheIndex <= limitToPrint)
            {
                return cache.Take(cacheIndex).ToArray();
            }
            // Else if there is more 'rows' in cache than max rows
            else
            {
                if (!manualPaging)
                    return cache.Skip(cacheIndex - limitToPrint).Take(limitToPrint).ToArray();
                else
                {
                    int offset = cacheIndex + pageOffset - limitToPrint;
                    if (offset < 0) offset = 0;
                    if (offset >= cacheIndex) offset = cacheIndex - limitToPrint;
                    return cache.Skip(offset).Take(limitToPrint).ToArray();
                }        
            }   
        }
    }
}
