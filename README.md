# Colorblind+

Adds colorblind labels to Stir Fry (and potentially more).

![preview](./img/preview.png)

For Stir Fry, uses the familiar B/C/Me labels for the main dish. The side of broccoli will be displayed as "-b".

Note: The title screen will not reflect the new labels.

## How colorblind labels work

This is mostly in case I forget, but:

* `ItemGroupView`s have two relevant properties:
    * `List<ColourBlindLabel> ComponentLabels` - list that gets loaded mysteriously, with `Item`s and their one or two letter abbreviations, storing the letter abbreviations for each component in the dish
    * `TextMeshPro ColourblindLabel` object, which is what displays the text. This seemingly only set when the dish has colorblind support built into the game, and is null for ItemGroupViews not supported in game.
* Additionally, the prefab of the `Item` that needs color blind support need a `Colour Blind` child containing a TextMeshPro. Not all Items have this.

This mod works by:
1. Taking the existing `Colour Blind` child from Pie
2. Cloning that child and adding it to an `Item`
3. Grabbing the `TextMeshPro` out of the cloned `Colour Blind` and setting it on the `Item`'s `ItemGroupView.ColourblindLabel`
4. Setting the `ItemGroupView.ComponentLabels` property with a custom list of Items/abbreviations
