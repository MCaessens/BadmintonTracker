let vue = new Vue({
    el: '#app',
    data: {
        loadedPokemon: null,
        searchQuery: "",
        modalTitle: "",
        modalMessage: "",
        isSearching: false
    },
    methods: {
        findPokeAsync: async function () {
            let app = this;
            app.isSearching = true;
            let searchParam = app.searchQuery === "" ? Math.round(Math.random() * pokeAmount) : app.searchQuery.toLowerCase();
            let response = await axios.get(`https://pokeapi.co/api/v2/pokemon/${searchParam}`)
                .then(resp => resp.data)
                .catch(() => {
                    app.modalTitle = "Not found";
                    app.modalMessage = `Pokemon with name or id: "${app.searchQuery}" was not found. Please try again.`;
                    $('#notificationModal').modal('show');
                    app.isSearching = false;
                    app.searchQuery = "";
                });

            if (response === undefined) return;
            app.isSearching = false;
            app.searchQuery = "";
            app.loadedPokemon = response;
        },
    },
    computed: {
        pokemonName() {
            let pokemonName = this.loadedPokemon.name;
            return toCapitalFirstLetter(pokemonName);
        },
        pokemonTypes() {
            let pokemonTypes = this.loadedPokemon.types.map(typeObj => toCapitalFirstLetter(typeObj.type.name));
            return pokemonTypes.join(", ");
        }
    }
});