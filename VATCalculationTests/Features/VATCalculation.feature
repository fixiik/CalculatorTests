Feature: VAT Calculation

Scenario: Verify base and reduced VAT calculation for <Country> with <VAT Rate>
    When I select "<Country>" from the country drop-down
    And  I check the "<VAT Rate>" checkbox
    And  I enter "<Amount>" in the "<Input Type>" field
    Then the system should calculate the "<Expected Result>" in the "<Expected Output>" field

    Examples:
    | Country        | VAT Rate | Input Type | Amount | Expected Result | Expected Output |
    | Germany        | 19%      | NetPrice   | 100    | 119.00          | Price           |
    | Germany        | 7%       | NetPrice   | 100    | 107.00          | Price           |
    | United Kingdom | 20%      | NetPrice   | 100    | 120.00          | Price           |
    | United Kingdom | 5%       | NetPrice   | 100    | 105.00          | Price           |

Scenario: Verify when user enters one amount in any field, the other 2 amounts are calculated properly
    When I select "United Kingdom" from the country drop-down
    And  I check the "<Checkbox>" checkbox
    And  I enter "<Value>" in the "<Input Type>" field
    Then the system should calculate the "<Price>" in the "Price" field
    Then the system should calculate the "<VAT>" in the "VATsum" field
    Then the system should calculate the "<NetAmount>" in the "NetPrice" field

    Examples:
    | Checkbox              | Input Type | Value     | NetAmount | Price         | VAT           |
    | Price without VAT     | NetPrice   | 999999999 | 999999999 | 1199999998.80 | 199999999.80  |
    | Price incl. VAT       | Price      | 25.36     | 21.13     | 25.36         | 4.23          |
    | Value-Added Tax       | VATsum     | 0.01      | 0.05      | 0.06          | 0.01          |