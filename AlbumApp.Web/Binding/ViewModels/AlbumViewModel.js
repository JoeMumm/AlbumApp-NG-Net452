
appAlbumModule.controller("AlbumViewModel", function ($scope, $http,
              viewModelHelper, uiGridConstants)
{
  //$locationProvider.html5Mode({ enabled: true, requireBase: false });
  $scope.viewModelHelper = viewModelHelper;
  viewModelHelper.isLoading = true;

  var filters = new AlbumApp.AlbumFilter; 
  var initializeFilters = function () {
    filters.AlbumNumber = ''; filters.Title = ''; filters.Genre = 0;
    filters.Artist = ''; filters.Price = -1; filters.StockAmount = -1; }
  initializeFilters();
  var sortArray = [];
  var allowSort = true;
  var paginationOptions = { pageNumber: 1, pageSize: 8 };

  $scope.currentPage = 1;
  $scope.pageSize = paginationOptions.pageSize;

  var getAlbums = function () { /* api/album/getallalbumspaged/ */
    var uri = (paginationOptions.pageNumber - 1) + '/' + paginationOptions.pageSize;
    var sortList = JSON.stringify(sortArray);
    var filtersJSON = JSON.stringify(filters); // angular.toJson(filter, false)
    viewModelHelper.apiGet('api/album/getallalbumspaged/' + uri,
      { params: { filters: filtersJSON, sorts: sortList } },
      function (result) {
        var albums = [];
        result.data.albums.forEach((entry) => { 
          var albumDisplayViewModel = { AlbumId: entry.AlbumId, Title: entry.Title,  Artist: entry.Artist, 
            Genre: entry.Genre, GenreText: getGenre(entry.Genre), AlbumNumber: entry.AlbumNumber,
            Price: entry.Price, StockAmount: entry.StockAmount, Image: entry.Image };
          albums.push(albumDisplayViewModel);
        });
        $scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);
        $scope.gridOptions.data = albums;
        $scope.gridOptions.totalItems = result.data.totalCount;
      });
  }

  getAlbums();

  $scope.select = function (AlbumId) {
    window.location.href = AlbumApp.rootPath + 'Album/Details/' + AlbumId;
  };

  $scope.gridOptions = {
    enableSorting: true, 
    paginationPageSizes: [5, 8, 15, 20, 25, 50, 100],
    paginationPageSize: paginationOptions.pageSize,
    useExternalPagination: true,
    useExternalSorting: true,
    useExternalFiltering: true,
    enableFiltering: true,
    enableGridMenu: true,
    rowHeight: 55,
    columnDefs: [   
      { name: 'Select', field: 'Image', width: 55, enableHiding: false, enableSorting: false,
        enableColumnMenu: false, enableFiltering: false, 
        cellTemplate: '<button ng-click="grid.appScope.select(row.entity.AlbumId)" style="padding:0px">' +
        '<img width=\"50px\" ng-src=\"{{grid.getCellValue(row, col)}}\" lazy-src></button>' },
      { name: 'Title', field: 'Title', width: 260, enableHiding: false },
      { name: 'Artist', field: 'Artist', width: 210, enableHiding: false },
      { name: 'Genre', field: 'GenreText', width: 140, enableHiding: false,
        filter: { type: uiGridConstants.filter.SELECT,
          selectOptions: [ { value: '1', label: 'Afro-Cuban Jazz' },
              { value: '2', label: 'Brazilian Jazz' },
              { value: '3', label: 'Classical' },
              { value: '4', label: 'Jazz' },
              { value: '5', label: 'Rock and Roll' } ] } },
      { name: 'Album Number', field: 'AlbumNumber', width: 130, enableHiding: false },
      { name: 'Price', field: 'Price', width: 75, enableHiding: false, cellFilter: 'number: 2' },
      { name: 'Stock Qty', field: 'StockAmount', width: 115, enableHiding: false }],
    gridMenuCustomItems: [{ title: 'Clear all sorting',
      action: function ($event) { allowSort = false;
          angular.forEach($scope.gridApi.grid.columns, function (col) {
            col.unsort(); });     sortArray = [];
          allowSort = true; getAlbums(); }, order: 110 }],
    onRegisterApi: function (gridApi) {
      $scope.gridApi = gridApi;
      gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
        paginationOptions.pageNumber = newPage;
        paginationOptions.pageSize = pageSize;
        $scope.pageSize = pageSize;
        $scope.currentPage = newPage;
        $scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);
        getAlbums(); });
      gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
        if (allowSort) {
          sortArray = [];
          sortColumns.forEach(getSorts);
          getAlbums();
        }  });

    }
  };

  var getSorts = function (item) {
    if (item.field == 'GenreText') item.field = 'Genre';
    var sortItem = new AlbumApp.SortItem; sortItem.Column = item.field;
    sortItem.Direction = (item.sort.direction == 'asc') ? 0 : 1;
    sortArray.push(sortItem); }

  $scope.applyFilters = function () {
    var grid = this.gridApi.grid;
    initializeFilters();
    angular.forEach(grid.columns, function (value, key) {
      if (value.filters) {
        if (value.filters[0].term) {
          setFilters(value.colDef.name, value.filters[0].term);
        } } });
    getAlbums(); }

  var setFilters = function (name, term) {
    switch (name) {
      case 'Title': filters.Title = term; break;
      case 'Artist': filters.Artist = term; break;
      case 'Genre': filters.Genre = term; break;
      case 'AlbumNumber': filters.AlbumNumber = term; break;
      case 'Price': filters.Price = term; break;
      case 'Stock Qty': filters.StockAmount = term; break;
      default: break; } }

  var getGenre = function (genreNumber) {
    var genre = '';
    switch (genreNumber) {
      case 1: genre = "Afro-Cuban Jazz"; break;
      case 2: genre = "Brazilian Jazz"; break;
      case 3: genre = "Classical"; break;
      case 4: genre = "Jazz"; break;
      case 5: genre = "Rock and Roll"; break;
      default: genre = ""; break; }
    return genre; }

});