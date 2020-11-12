exports.rules = {
    "block-no-empty": null,
    "color-no-invalid-hex": true,
    "length-zero-no-unit": true,
    "shorthand-property-no-redundant-values": true,
    "declaration-block-no-duplicate-properties": [ true, {
       "ignore": ["consecutive-duplicates-with-different-values"]
    } ],
    "declaration-block-trailing-semicolon": "always",
    "declaration-block-single-line-max-declarations": 1,
    "declaration-block-semicolon-space-before": "never",
    "declaration-block-no-redundant-longhand-properties": true,
    "declaration-colon-space-after": "always",
    "selector-type-case": "lower",
    "no-duplicate-selectors": true,
    "no-extra-semicolons": true,
    "max-empty-lines": 1,
    "property-case": "lower",
    "value-list-comma-space-after": "always",
    "selector-max-id": 0
};

exports.ignoreFiles = "./src/vendors/**/*";
