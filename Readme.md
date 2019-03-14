# Kata For Code ComputerLove



A simple Checkout that accepts scanned items and calculates a total based on the items and any offers.



### Assumptions

- An item can only appear in a single deal (this is consistent with other Checkout systems I have encountered in the world).

- Certain deals should have precedence over others, which relates to the point above.



### Notes

- I have not used async/await in this project, despite mocked access of data stores. This was conscious decision and I would always for any potentially external resource. However for an example of TDD it would have cluttered the rest of the code, having ValueTask and Task everywhere and all tests wrapped in Actions.

- The checkout works by maintaining a list of products that have been scanned. After each scan the products are all sent through a set of offer matchers, that identify items that belong to an offer. It returns a list of price modifiers, calculated from the items found. The checkout then adjusts the raw total of all products by all of the price modifiers.



### Improvements

- The next stage of this Checkout would be a refactor stage, including:

  - Making certain members internal only. This is very important as it reduces the external API surface to what is 'needed'. That helps reduce tech debt over time as it means only worrying about the API exterior when it comes to changes.

  - Providing a new offer types, such as a mix and match offer.

  - Remaining classes properly, as at the moment noun and verbs are not used correctly and the classes/interfaces should be named better

  - Ensure comments where needed. So:

    - Cleaning up comments where there has been a change.

    - Removing comments that are explained by naming, to reduce visual clutter

    - Adding new comments where the logic isnt obvious or there are interfaces

- Ideally I would have returned a 'checkout breakdown' so that it could give a full output to the user, rather than just the total. I have returned objects where I can to give the ability to return other information

- I would be more consistent with 'internal' use of concrete classes like List<T> and IReadOnlyList<T>. As per SOLID "I" and "D" it is important to use interfaces so you can feed in processes and with "S" have a class/struct only focus on what it needs, and supply the rest via a constructor etc etc. However as with everything in the world there is a trade off in performance. Most notably boxing/unboxing, however that often doesnt impact applications unless they are under extreme load, but the performance difference between enumerating a IEnumerable (or IReadOnlyList) as opposed to a list can be extreme. However this refactoring only becomes necessary if the code is slow in the environment it is used, over optimisation can in itself be a killer.





By Lee Cooper