using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using PBASE.Entity;
using System.Data;
using System.Linq.Expressions;
using Probase.GridHelper;
using System.Threading.Tasks;

namespace PBASE.Repository.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private AppContext dataContext;
        private readonly IDbSet<T> dbset;

        public string ExceptionDetails { get; set; }

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected AppContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(T entity, object model)
        {
            dbset.Attach(entity);
            if (model != null)
            {
                foreach (var property in model.GetType().GetProperties().Where(x => x.Name != "FormMode" && x.Name != "FormName" && x.Name != "GridName" && x.Name != "IsCreateAnother"))
                {
                    try
                    {
                        dataContext.Entry(entity).Property(property.Name).IsModified = true;
                    }
                    catch (Exception)
                    {
                        // Model must have a different property. Eat up exception.
                        // Skip!
                    }
                }
            }
            else
            {
                dataContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T SelectById(long id)
        {
            return dbset.Find(id);
        }

        public virtual T SelectById(string id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> SelectAll()
        {
            return dbset.ToList();
        }

        public virtual List<T> SelectByGridSetting(GridSetting gridSetting)
        {
            IQueryable<T> query = from view in dataContext.Set<T>() select view;
            gridSetting.IsSearch = true;
            return SelectDataForGrid(query, gridSetting);
        }

        public virtual IEnumerable<T> SelectMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public T Select(Expression<Func<T, bool>> where)
        {

            return dbset.Where(where).FirstOrDefault<T>();
        }

        private bool IsRuleOfTypeString(Probase.GridHelper.Rule item)
        {
            //if (
            //    (item.field.Equals("ProductId")) ||
            //    (item.field.Equals("ProductName")) ||
            //    (item.field.Equals("PriceListStart")) ||
            //    (item.field.Equals("PriceListEnd")) ||
            //    (item.field.Equals("CustomerName")) ||
            //    (item.field.Equals("Class")) ||
            //    (item.field.Equals("ProductClass")) ||
            //    (item.field.Equals("Brand")) ||
            //    (item.field.Equals("PriceTypeShort")) ||
            //    (item.field.Equals("Contents")) ||
            //    (item.field.Equals("Status")) ||
            //    (item.field.Equals("ShortRef")) ||
            //    (item.field.Equals("SelectedStartDate")) ||
            //    (item.field.Equals("SelectedEndDate")) ||
            //    (item.field.Equals("StartDate")) ||
            //    (item.field.Equals("EndDate")) ||
            //    (item.field.Equals("ManufacturerName")) ||
            //    (item.field.Equals("Category")) ||
            //    (item.field.Equals("Models")) ||
            //    (item.field.Equals("CoverageDescription")) ||
            //    (item.field.Equals("ProductStatusShort"))
            //    )
            //{
            //    return true;
            //}
            //else
            //{
                return false;
            //}
        }

        protected string GetSQLOperator(Probase.GridHelper.Rule item)
        {
            string op = "";

            if (IsRuleOfTypeString(item))
            {
                switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), item.op)))
                {
                    case WhereOperation.Equal:
                        op = " = ";
                        break;
                    case WhereOperation.Contains:
                        op = " LIKE ";
                        break;
                    case WhereOperation.NotEqual:
                        op = " <> ";
                        break;
                    case WhereOperation.LessOrEqual:
                        op = " <= ";
                        break;
                    case WhereOperation.GreaterOrEqual:
                        op = " >= ";
                        break;
                    default:
                        op = " = ";
                        break;
                }
            }
            else
            {
                switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), item.op)))
                {
                    case WhereOperation.Equal:
                    case WhereOperation.Contains:
                        op = " = ";
                        break;
                    case WhereOperation.NotEqual:
                        op = " <> ";
                        break;
                    case WhereOperation.LessOrEqual:
                        op = " <= ";
                        break;
                    case WhereOperation.GreaterOrEqual:
                        op = " >= ";
                        break;
                    default:
                        op = " = ";
                        break;
                }
            }

            return op;
        }

        protected List<Probase.GridHelper.Rule> GetRuleList(GridSetting gridSetting)
        {
            List<Probase.GridHelper.Rule> rules = new List<Probase.GridHelper.Rule>();

            // Quick filter.
            rules = gridSetting.QuickSearchWhereList;

            // Advance filter.
            if (gridSetting.Where != null)
            {
                foreach (var rule in gridSetting.Where.rules)
                {
                    rules.Add(rule);
                }
            }

            return rules;
        }

        protected string ConvertRuleToString(Probase.GridHelper.Rule item)
        {
            string op = GetSQLOperator(item);
            string quote = string.Empty;
            string percentSign = string.Empty;

            if (op.Equals(" LIKE "))
            {
                quote = "'";
                percentSign = "%";
            }
            else
            {
                quote = IsRuleOfTypeString(item) ? "'" : "";
            }

            return item.field + op + quote + percentSign + item.data + percentSign + quote + " AND ";
        }

        protected string ConvertRuleToString(string fieldName, Probase.GridHelper.Rule item)
        {
            string quote = IsRuleOfTypeString(item) ? "'" : "";
            string op = GetSQLOperator(item);
            string percentSign = op.Equals(" LIKE ") ? "%" : "";
            return fieldName + op + quote + percentSign + item.data + percentSign + quote + " AND ";
        }

        protected IQueryable<T> ProcessCommonFields(Probase.GridHelper.Rule rule, IQueryable<T> query)
        {
            // Special case for handling empty or null values.
            if (rule.data == " ")
            {
                rule.data = null;
                query = query.Where<T>(rule.field, rule.data, WhereOperation.Equal);
            }
            else if (rule.data == "")
            {
                // ignore
            }
            else
            {
                query = query.Where<T>(rule.field, rule.data, (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
            }
            return query;
        }

        private IQueryable<T> ProcessComplexSearch(IQueryable<T> query, Filter filter)
        {
            if (filter.groupOp == "AND")
            {
                if (filter.rules != null)
                {
                    foreach (var rule in filter.rules)
                    {
                        query = ProcessFields(rule, query);
                    }
                }
            }
            else
            {

                object temp = null;
                foreach (var rule in filter.rules)
                {
                    if (temp == null)
                    {
                        temp = ProcessFields(rule, query);
                    }
                    else
                    {
                        var t = ProcessFields(rule, query);
                        temp = ((IQueryable<T>)temp).Concat<T>(t);
                    }
                }

                query = ((IQueryable<T>)temp).Distinct<T>();
            }

            return query;
        }

        protected virtual List<T> SelectDataForGrid(IQueryable<T> query, GridSetting gridSetting)
        {
            //filtering
            if (gridSetting.IsSearch)
            {
                // Quick search...
                // Quick search could be used as a part of original filter for grid.
                foreach (var rule in gridSetting.QuickSearchWhereList)
                {
                    query = ProcessFields(rule, query);
                }

                if (gridSetting.Where != null)
                {
                    IQueryable<T> originalQuery = query;

                    query = ProcessComplexSearch(query, gridSetting.Where);

                    if (gridSetting.Where.groups != null)
                    {
                        foreach (var item in gridSetting.Where.groups)
                        {
                            var t = ProcessComplexSearch(originalQuery, item);
                            query = query.Union(t);
                        }
                    }

                }
            }

            //sorting
            query = query.OrderBy<T>(MapToEntityColumnName(gridSetting.SortColumn), gridSetting.SortOrder);

            //count
            gridSetting.Count = query.Count();

            // Special case for handling loadOnce grids.
            if (gridSetting.SpecialTotalRows > 0)
            {
                gridSetting.PageSize = gridSetting.Count;
            }

            if (!gridSetting.IsExcelExport)
            {
                return query.Skip((gridSetting.PageIndex - 1) * gridSetting.PageSize).Take(gridSetting.PageSize).ToList();
            }
            else
            {
                // Note: CSV Export will always export complete data. It will not honor grid paging.
                return query.ToList();
            }

        }

        private string MapToEntityColumnName(string columnName)
        {
            if (columnName.Contains("_"))
            {
                return columnName.Substring(columnName.IndexOf("_") + 1).Trim();
            }
            else if (columnName.Contains(","))
            {
                return columnName.Substring(columnName.IndexOf(",") + 1).Trim();
            }
            else
            {
                return columnName;
            }
        }

        protected virtual IQueryable<T> ProcessFields(Probase.GridHelper.Rule rule, IQueryable<T> query)
        {
            return ProcessCommonFields(rule, query);
        }

        #region Async Mehtods

        public async virtual Task<T> SelectByIdAsync(long id)
        {
            return await Task.FromResult<T>(dbset.Find(id));
        }

        public async virtual Task<IEnumerable<T>> SelectAllAsync()
        {
            return await dbset.ToListAsync<T>();
        }

        public async virtual Task<IEnumerable<T>> SelectManyAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.Where(where).ToListAsync<T>();
        }

        public async Task<T> SelectAsync(Expression<Func<T, bool>> where)
        {

            return await dbset.Where(where).FirstOrDefaultAsync<T>();
        }

        public async virtual Task<int> TotalAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.CountAsync<T>(where);
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<T, bool>> where)
        {
            return dbset.AnyAsync<T>(where);
        }

        public virtual Task<List<T>> SelectByGridSettingAsync(GridSetting gridSetting)
        {
            IQueryable<T> query = from view in dataContext.Set<T>() select view;
            gridSetting.IsSearch = true;
            return SelectDataForGridAsync(query, gridSetting);
        }

        protected virtual Task<List<T>> SelectDataForGridAsync(IQueryable<T> query, GridSetting gridSetting)
        {
            try
            {
                //filtering
                if (gridSetting.IsSearch)
                {
                    // Quick search...
                    // Quick search could be used as a part of original filter for grid.
                    foreach (var rule in gridSetting.QuickSearchWhereList)
                    {
                        query = ProcessFields(rule, query);
                    }

                    if (gridSetting.Where != null)
                    {
                        IQueryable<T> originalQuery = query;

                        query = ProcessComplexSearch(query, gridSetting.Where);

                        if (gridSetting.Where.groups != null)
                        {
                            foreach (var item in gridSetting.Where.groups)
                            {
                                var t = ProcessComplexSearch(originalQuery, item);
                                query = query.Union(t);
                            }
                        }

                    }
                }

                //sorting
                query = query.OrderBy<T>(MapToEntityColumnName(gridSetting.SortColumn), gridSetting.SortOrder);

                //count
                gridSetting.Count = query.Count();

                // Special case for handling loadOnce grids.
                if (gridSetting.SpecialTotalRows > 0)
                {
                    gridSetting.PageSize = gridSetting.Count;
                }

                if (!gridSetting.IsExcelExport)
                {
                    return query.Skip((gridSetting.PageIndex - 1) * gridSetting.PageSize).Take(gridSetting.PageSize).ToListAsync();
                }
                else
                {
                    // Note: CSV Export will always export complete data. It will not honor grid paging.
                    return query.ToListAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }


        #endregion



    }
}
