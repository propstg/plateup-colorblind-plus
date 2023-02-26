import {LitElement, html, css} from 'https://cdn.jsdelivr.net/gh/lit/dist@2/core/lit-core.min.js';

export class ColorblindApp extends LitElement {
    static styles = css`
        .hidden { display: none; }
    `;

    static properties = {
        values: {type: Array}
    };

    constructor() {
        super();
        this.values = [];
    }

    loadDataFromClipboard() {
        const data = this.renderRoot?.querySelector("#dataFromGame").value ?? "";
      
        if (data.indexOf("CB+ALLITEMS---") < 0 || data.indexOf("---CB+CURRENT---") < 0) {
            alert("Invalid input");
            return;
        }
      
        const [availableInput, currentBlacklistInput] = data.substring("CB+ALLITEMS---".length).split("---CB+CURRENT---");
        const currentBlacklist = currentBlacklistInput.split(",");
        this.values = availableInput.split(";;;").map(itemCombination => {
            const [id, name] = itemCombination.split(":::");
            return {id, name, selected: currentBlacklist.includes(id), visible: true};
        });
    }

    updateFilter() {
        const filter = this.renderRoot?.querySelector("#filter").value ?? "";
        this.values = this.values.map(item => {
            item.visible = item.name.toLowerCase().includes(filter.toLowerCase());
            return item;
        });
    }

    toggleChecked(e) {
        this.values = this.values.map((item, index) => {
            if (index === +e.target.value) {
                item.selected = !item.selected;
            }
            return item;
        });
    }

    selectAllVisible() {
        this.values = this.values.map(item => {
            if (item.visible) {
                item.selected = true;
            }
            return item;
        });
    }

    deselectAllVisible() {
        this.values = this.values.map(item => {
            if (item.visible) {
                item.selected = true;
            }
            return item;
        });
    }

    render() {
        if (this.values.length === 0) {
            return html`
                <h2>Blacklist colorblind labels for specific items</h2>
                <ol>
                    <li>In PlateUp!, go to Mod Preferences > Colorblind+ > Experimental</li>
                    <li>Choose the "Copy Item Data" option to copy the item data to your clipboard</li>
                    <li>Paste here: <input type="text" id="dataFromGame" @input=${this.loadDataFromClipboard}/>
                </ol>
            `;
        }
      
        return html`
            <h2>Blacklist colorblind labels for specific items</h2>
            <ol>
                <li>Check which items you want to remove labels from</li>
                <li>Copy the string in the textarea at the bottom</li>
                <li>In PlateUp!, go to Mod Preferences > Colorblind+ > Experimental and click the "Update Blacklist from Clipboard" option.</li>
            </ol>
            
            <div>
                <button @click=${this.selectAllVisible}>Select all visible</button>
                <button @click=${this.deselectAllVisible}>Deselect all visible</button>
            </div>
            
            <div>
                <label for="filter">Filter: </label>
                <input type="text" id="filter" @input=${this.updateFilter}/>
            </div>
            
            ${this.values.map((item, index) => html`
                <div class=${item.visible ? "" : "hidden"}>
                    <input type="checkbox" id="#item${index}" .checked=${item.selected} value=${index} @click=${this.toggleChecked} />
                    <label for="#item${index}">${item.name}</label>
                </div>
            `)}
            <textarea>CB+OFF---${this.values.filter(item => item.selected).map(item => item.id).join(",")}</textarea>
        `;
    }
}
customElements.define('colorblind-app', ColorblindApp);