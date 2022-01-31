let vue = new Vue({
    el: '#app',
    data: {
        loggedIn: false,
        gamesLoaded: false,
        games: null,
        selectedGame: null,
        token: undefined,
        config: null,
        modalMessage: "",
        modalTitle: "",
        newGame: null,
        locations: null,
        rackets: null,
        shuttles: null,
        isAdding: false,
        isUpdating: false,
        isDeleting: false,
        pageInfo: {
            pageAmount: 0,
            itemCount: 0
        },
    },
    methods: {
        loadGamesAsync: async function () {
            let app = this;
            app.token = getCookieValue("auth-token");
            if (app.token === undefined) return;

            app.loggedIn = true;
            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            app.locations = await axios.get(`${apiLink}me/locations`, app.config)
                .then(resp => resp.data.results)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            app.rackets = await axios.get(`${apiLink}me/rackets`, app.config)
                .then(resp => resp.data.results)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            app.shuttles = await axios.get(`${apiLink}me/shuttleCocks`, app.config)
                .then(resp => resp.data.results)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            let response = await axios.get(`${apiLink}me/games`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.games = response.results;
            app.pageInfo = response.info;
            app.gamesLoaded = true;
        },
        deleteAsync: async function () {
            let app = this;
            app.isDeleting = true;
            let response = await axios.delete(`${apiLink}me/games${app.selectedGame.id}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isDeleting = false;
                    app.modalTitle = "Deletion Failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isDeleting = false;
            app.games.pop(app.selectedGame);
            app.pageInfo.itemCount--;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.selectedGame = null;
        },
        updateAsync: async function () {
            let app = this;
            app.isUpdating = true;
            let response = await axios.put(`${apiLink}me/games`, app.selectedGame, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isUpdating = false;
                    app.modalTitle = "Update failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isUpdating = false;
            let gameIds = app.games.map(game => game.id);
            let index = gameIds.indexOf(app.selectedGame.id);
            app.games.splice(index, 1, response);
            app.selectedGame = null;
        },
        addAsync: async function () {
            let app = this;
            app.isAdding = true;
            let response = await axios.post(`${apiLink}me/games`, app.newGame, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.isAdding = false;
                    app.modalTitle = "Add failed";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.isAdding = false;
            app.games.push(response);
            app.games = app.games.splice(0, 10);
            app.pageInfo.itemCount++;
            app.pageInfo.pageAmount = toPageAmount(app.pageInfo.itemCount);
            app.newGame = null;
        },
        loadGamesByPage: async function (page) {
            let app = this;
            app.locationsLoaded = false;
            app.config = {
                headers: {
                    Authorization: `Bearer ${app.token}`,
                }
            };
            let response = await axios.get(`${apiLink}me/games?Page=${page}`, app.config)
                .then(resp => resp.data)
                .catch((error) => {
                    app.modalTitle = "Error";
                    app.modalMessage = handleError(error.response.data);
                    $('#notificationModal').modal('show');
                });
            if (response === undefined) return;

            app.pageInfo = response.info;
            app.games = response.results;
            app.locationsLoaded = true;
        }
    },
    mounted() {
        this.loadGamesAsync();
    }
});